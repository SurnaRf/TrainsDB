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
using MessagePack;

namespace TrainsMVC.Controllers
{
    public class LocomotivesController : Controller
    {
        private readonly GenericManager<Locomotive, int>       locomotiveManager;
        private readonly GenericManager<Location, int>         locationManager;
        private readonly GenericManager<TrainComposition, int> trainCompositionManager;

        public LocomotivesController(
            GenericManager<Locomotive, int>       locomotiveManager,
            GenericManager<Location, int>         locationManager,
            GenericManager<TrainComposition, int> trainCompositionManager)
        {
            this.locomotiveManager       = locomotiveManager;
            this.locationManager         = locationManager;
            this.trainCompositionManager = trainCompositionManager;
        }
        

        // GET: Locomotives
        public async Task<IActionResult> Index()
        {
            return View(await locomotiveManager.ReadAllAsync(true));
        }

        // GET: Locomotives/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locomotive = await locomotiveManager.ReadAsync((int)id, true);
            if (locomotive == null)
            {
                return NotFound();
            }

            return View(locomotive);
        }

        // GET: Locomotives/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigation();
            return View();
        }

        // POST: Locomotives/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nickname,CarryingCapacity,LocomotiveType,LocationId,TrainCompositionId")] Locomotive locomotive)
        {
            await MakeValid(locomotive);
            ModelState.Clear();
            if (locomotive.Location != null)
            {
                await locomotiveManager.CreateAsync(locomotive);
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigation(locomotive);
            return View(locomotive);
        }

        // GET: Locomotives/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locomotive = await locomotiveManager.ReadAsync((int)id, true, false);
            if (locomotive == null)
            {
                return NotFound();
            }
            await LoadNavigation(locomotive);
            return View(locomotive);
        }

        // POST: Locomotives/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nickname,CarryingCapacity,LocomotiveType,LocationId,TrainCompositionId")] Locomotive locomotive)
        {
            if (id != locomotive.Id)
            {
                return NotFound();
            }

            
            
            await MakeValid(locomotive);
            if (locomotive.Location != null)
            {
                try
                {
                    await locomotiveManager.UpdateAsync(locomotive, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LocomotiveExists(locomotive.Id))
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


            await LoadNavigation(locomotive);
            return View(locomotive);
        }

        // GET: Locomotives/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locomotive = await locomotiveManager.ReadAsync((int)id);
            if (locomotive == null)
            {
                return NotFound();
            }

            return View(locomotive);
        }

        // POST: Locomotives/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await locomotiveManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> LocomotiveExists(int id)
        {
            return (await locomotiveManager.ReadAsync(id)) != null;
        }

        private async Task MakeValid(Locomotive locomotive)
        {
            ModelState.Clear();

            bool hasComposition = !(locomotive.TrainCompositionId == TrainComposition.NoneId);
            if (hasComposition)
            {
                locomotive.TrainComposition
                    = await trainCompositionManager.ReadAsync((int)locomotive.TrainCompositionId!);

                if (locomotive.TrainComposition != null)
                {
                    locomotive.LocationId = locomotive.TrainComposition.LocationId;
                }
            }
            else
            {
                locomotive.TrainCompositionId = null;
                locomotive.TrainComposition = null;
            }

            locomotive.Location = await locationManager.ReadAsync(locomotive.LocationId);
            
            ModelState.Clear();
        }

        private async Task LoadNavigation(Locomotive? selectedValues = null)
        {
            var locations = await locationManager.ReadAllAsync();
            
            var trainCompositions = (await trainCompositionManager.ReadAllAsync()).ToList();
            trainCompositions.Insert(0, TrainComposition.None);

            if(selectedValues == null)
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
