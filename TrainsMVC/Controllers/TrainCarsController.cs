using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using ServiceLayer;

namespace TrainsMVC.Controllers
{
    public class TrainCarsController : Controller
    {
        private readonly GenericManager<TrainCar, int>         trainCarManager;
        private readonly GenericManager<Location, int>         locationManager;
        private readonly GenericManager<TrainComposition, int> trainCompositionManager;

        public TrainCarsController(
            GenericManager<TrainCar, int>         trainCarManager,
            GenericManager<Location, int>         locationManager,
            GenericManager<TrainComposition, int> trainCompositionManager)
        {
            this.trainCarManager         = trainCarManager;
            this.locationManager         = locationManager;
            this.trainCompositionManager = trainCompositionManager;
        }

        // GET: TrainCars
        public async Task<IActionResult> Index()
        {
            return View(await trainCarManager.ReadAllAsync(true));
        }

        // GET: TrainCars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainCar = await trainCarManager.ReadAsync((int)id, true);
            if (trainCar == null)
            {
                return NotFound();
            }

            return View(trainCar);
        }

        // GET: TrainCars/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigation();
            return View();
        }

        // POST: TrainCars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainCarType,Weight,LocationId,TrainCompositionId")] TrainCar trainCar)
        {
            await MakeValid(trainCar);
            ModelState.Clear();
            if (trainCar.Location != null)
            {
                await trainCarManager.CreateAsync(trainCar);
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigation(trainCar);
            return View(trainCar);
        }

        // GET: TrainCars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainCar = await trainCarManager.ReadAsync((int)id, true);
            if (trainCar == null)
            {
                return NotFound();
            }
            await LoadNavigation(trainCar);
            return View(trainCar);
        }

        // POST: TrainCars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainCarType,Weight,LocationId,TrainCompositionId")] TrainCar trainCar)
        {
            if (id != trainCar.Id)
            {
                return NotFound();
            }

            await MakeValid(trainCar);
            ModelState.Clear();
            if (trainCar.Location != null)
            {
                try
                {
                    await trainCarManager.UpdateAsync(trainCar);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrainCarExists(trainCar.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigation(trainCar);
            return View(trainCar);
        }

        // GET: TrainCars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainCar = await trainCarManager.ReadAsync((int)id);
            if (trainCar == null)
            {
                return NotFound();
            }

            return View(trainCar);
        }

        // POST: TrainCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await trainCarManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrainCarExists(int id)
        {
            return (await trainCarManager.ReadAsync(id)) != null;
        }

        private async Task MakeValid(TrainCar trainCar)
        {
            trainCar.Location = await locationManager.ReadAsync(trainCar.LocationId);

            bool hasComposition = !(trainCar.TrainCompositionId == null || trainCar.TrainCompositionId == TrainComposition.NoneId);
            if (!hasComposition)
            {
                trainCar.TrainCompositionId = null;
                trainCar.TrainComposition = null;
                return;
            }

            trainCar.TrainComposition = await trainCompositionManager.ReadAsync((int)trainCar.TrainCompositionId!);
        }

        private async Task LoadNavigation(TrainCar? selectedValues = null)
        {
            var locations = await locationManager.ReadAllAsync();

            var trainCompositions = (await trainCompositionManager.ReadAllAsync()).ToList();
            trainCompositions.Insert(0, TrainComposition.None);

            if (selectedValues == null)
            {
                ViewData["LocationId"] = new SelectList(locations, "Id", "Name");
                ViewData["TrainCompositionId"] = new SelectList(trainCompositions, "Id", "Name");
                return;
            }

            ViewData["LocationId"] = new SelectList(locations, "Id", "Name", selectedValues.LocationId);
            ViewData["TrainCompositionId"] = new SelectList(trainCompositions, "Id", "Name", selectedValues.TrainCompositionId);
        }
    }
}
