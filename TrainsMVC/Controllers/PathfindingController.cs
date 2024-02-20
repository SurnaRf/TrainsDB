using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BusinessLayer;
using ServiceLayer;
using TrainsMVC.Models;

namespace TrainsMVC.Controllers
{
    public class PathfindingController : Controller
    {
        private readonly GenericManager<Location, int> locationManager;
        private readonly GenericManager<Connection, int> connectionManager;

        public PathfindingController(
            GenericManager<Location, int> locationManager,
            GenericManager<Connection, int> connectionManager)
        {
            this.locationManager = locationManager;
            this.connectionManager = connectionManager;
        }

        public async Task<IActionResult> Pathfinder()
        {
            await LoadNavigation();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pathfinder([Bind("LocationFromId,LocationToId")] PathQuery pathQuery)
        {
            await MakeValid(pathQuery);
            await LoadNavigation();
            return View(pathQuery);
        }

        private async Task LoadNavigation()
        {
            var locations = await locationManager.ReadAllAsync();

            ViewData["LocationFromId"] = new SelectList(locations, "Id", "Name");
            ViewData["LocationToId"] = new SelectList(locations, "Id", "Name");
        }

        private async Task MakeValid(PathQuery pathQuery)
        {
            pathQuery.LocationFrom = await locationManager.ReadAsync(pathQuery.LocationFromId);
            pathQuery.LocationTo   = await locationManager.ReadAsync(pathQuery.LocationToId);
            
            pathQuery.Paths = await PathfindingUtility.GetPaths(pathQuery.LocationFrom, connectionManager);
            pathQuery.Path = pathQuery.Paths.ReconstructPathEdges(pathQuery.LocationTo);
        }
    }
}
