using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using DataLayer;
using ServiceLayer;

namespace TrainsMVC.Controllers
{
    public class TrainCompositionsController : Controller
    {
        private readonly GenericManager<TrainComposition, int> trainCompositionManager;
        private readonly GenericManager<Location, int> locationManager;

        public TrainCompositionsController(
            GenericManager<TrainComposition, int> trainCompositionManager,
            GenericManager<Location, int> locationManager)
        {
            this.trainCompositionManager = trainCompositionManager;
            this.locationManager = locationManager;
        }

        // GET: TrainCompositions
        public async Task<IActionResult> Index()
        {
            return View(await trainCompositionManager.ReadAllAsync(true));
        }

        // GET: TrainCompositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainComposition = await trainCompositionManager.ReadAsync((int)id, true);
            if (trainComposition == null)
            {
                return NotFound();
            }

            return View(trainComposition);
        }

        // GET: TrainCompositions/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigation();
            return View();
        }

        // POST: TrainCompositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TrainType,LocationId")] TrainComposition trainComposition)
        {
            await MakeValid(trainComposition);
            ModelState.Clear();
            if (TryValidateModel(trainComposition))
            {
                await trainCompositionManager.CreateAsync(trainComposition);
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigation(trainComposition);
            return View(trainComposition);
        }

        // GET: TrainCompositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainComposition = await trainCompositionManager.ReadAsync((int)id, true, false);
            if (trainComposition == null)
            {
                return NotFound();
            }
            await LoadNavigation(trainComposition);
            return View(trainComposition);
        }

        // POST: TrainCompositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,TrainType,LocationId")] TrainComposition trainComposition)
        {
            if (id != trainComposition.Id)
            {
                return NotFound();
            }

            await MakeValid(trainComposition);
            ModelState.Clear();
            if (TryValidateModel(trainComposition))
            {
                try
                {
                    await trainCompositionManager.UpdateAsync(trainComposition);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await TrainCompositionExists(trainComposition.Id))
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
            await LoadNavigation(trainComposition);
            return View(trainComposition);
        }

        // GET: TrainCompositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainComposition = await trainCompositionManager.ReadAsync((int)id, true);
            if (trainComposition == null)
            {
                return NotFound();
            }

            return View(trainComposition);
        }

        // POST: TrainCompositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await trainCompositionManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TrainCompositionExists(int id)
        {
            return (await trainCompositionManager.ReadAsync(id)) != null;
        }

        private async Task MakeValid(TrainComposition trainComposition)
        {
            trainComposition.Location = await locationManager.ReadAsync(trainComposition.LocationId);
        }

        private async Task LoadNavigation(TrainComposition? selectedValues = null)
        {
            var locations = await locationManager.ReadAllAsync();

            if (selectedValues == null)
            {
                ViewData["LocationId"] = new SelectList(locations, "Id", "Name");
                return;
            }

            ViewData["LocationId"] = new SelectList(locations, "Id", "Name", selectedValues.LocationId);
        }
    }
}
