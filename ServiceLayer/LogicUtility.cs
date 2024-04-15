using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BusinessLayer;
using DataLayer;

namespace ServiceLayer
{
    public class LogicUtility
    {
        public static async Task LoadNavigation(TrainDbContext dbContext, TrainComposition trainComposition)
        {
            await dbContext.Entry(trainComposition).Reference(trcn => trcn.Location).LoadAsync();
            await dbContext.Entry(trainComposition).Collection(trcn => trcn.Locomotives).LoadAsync();
            await dbContext.Entry(trainComposition).Collection(trcn => trcn.TrainCars).LoadAsync();
        }

        public static async Task UpdateLocationOfComposition(
            IDb<TrainComposition, int> compositionContext,
            IDb<Locomotive, int> locomotiveContext,
            IDb<TrainCar, int> trainCarContext,
            TrainComposition trainComposition,
            Location newLocation)
        {
            if (trainComposition.LocationId == newLocation.Id)
            {
                await compositionContext.UpdateAsync(trainComposition);
                return;
            }

            trainComposition = await compositionContext.ReadAsync(trainComposition.Id, true);

            bool isOnThatLocation = trainComposition.Location == newLocation;

            if(isOnThatLocation || !CanMove(trainComposition)) return;

            trainComposition.Location = newLocation;
            trainComposition.LocationId = newLocation.Id;
            await compositionContext.UpdateAsync(trainComposition, true);

            foreach(Locomotive locomotive in trainComposition.Locomotives)
            {
                locomotive.LocationId = newLocation.Id;
                await locomotiveContext.UpdateAsync(locomotive, true);
            }

            foreach(TrainCar trainCar in trainComposition.TrainCars)
            {
                trainCar.LocationId = newLocation.Id;
                await trainCarContext.UpdateAsync(trainCar, true);
            }
        }

        public static bool CanMove(TrainComposition trainComposition)
        {
            return TotalCarryingCapacity(trainComposition.Locomotives)
                >= trainComposition.TotalWeight();
        }

        public static double TotalCarryingCapacity(List<Locomotive> locomotives)
        {
            double totalCarryingCapacity = 0;
            foreach(Locomotive locomotive in locomotives)
            {
                totalCarryingCapacity += locomotive.CarryingCapacity;
            }
            return totalCarryingCapacity;
        }
    }
}
