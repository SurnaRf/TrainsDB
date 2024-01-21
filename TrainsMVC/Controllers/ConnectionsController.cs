using System;
using System.Diagnostics;
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
    public class ConnectionsController : Controller
    {
        private readonly GenericManager<Connection, int> connectionManager;
        private readonly GenericManager<Location, int> locationContext;

        public ConnectionsController(
            GenericManager<Connection, int> connectionManager,
            GenericManager<Location, int> locationManager)
        {
            this.connectionManager = connectionManager;
            locationContext = locationManager;
        }

        // GET: Connections
        public async Task<IActionResult> Index()
        {
            return View(await connectionManager.ReadAllAsync(true));
        }

        // GET: Connections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await connectionManager.ReadAsync((int)id, true);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // GET: Connections/Create
        public async Task<IActionResult> Create()
        {
            await LoadNavigation();
            return View();
        }

        // POST: Connections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TerrainType,NodeAId,NodeBId")] Connection connection)
        {
            await MakeValid(connection);
            ModelState.Clear();
            if (TryValidateModel(connection))
            {
                await connectionManager.CreateAsync(connection);
                return RedirectToAction(nameof(Index));
            }
            await LoadNavigation();
            return View(connection);
        }

        // GET: Connections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await connectionManager.ReadAsync((int)id, true, false);
            if (connection == null)
            {
                return NotFound();
            }
            await LoadNavigation();
            return View(connection);
        }

        // POST: Connections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TerrainType,NodeAId,NodeBId")] Connection connection)
        {
            if (id != connection.Id)
            {
                return NotFound();
            }

            await MakeValid(connection);
            ModelState.Clear();
            if (TryValidateModel(connection))
            {
                try
                {
                    await connectionManager.UpdateAsync(connection, true);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await ConnectionExists(connection.Id))
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
            await LoadNavigation();
            return View(connection);
        }

        // GET: Connections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var connection = await connectionManager.ReadAsync((int)id, true);
            if (connection == null)
            {
                return NotFound();
            }

            return View(connection);
        }

        // POST: Connections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await connectionManager.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ConnectionExists(int id)
        {
            return (await connectionManager.ReadAsync(id)) != null;
        }

        private async Task MakeValid(Connection connection)
        {
            connection.NodeA = await locationContext.ReadAsync(connection.NodeAId);
            connection.NodeB = await locationContext.ReadAsync(connection.NodeBId);
        }

        private async Task LoadNavigation(Connection? selectedValues = null)
        {
            var locations = await locationContext.ReadAllAsync();
            
            if(selectedValues == null)
            {
                ViewData["NodeAId"] = new SelectList(locations, "Id", "Name");
                ViewData["NodeBId"] = new SelectList(locations, "Id", "Name");
                return;
            }

            ViewData["NodeAId"] = new SelectList(locations, "Id", "Name", selectedValues.NodeAId);
            ViewData["NodeBId"] = new SelectList(locations, "Id", "Name", selectedValues.NodeBId);
        }
    }
}
