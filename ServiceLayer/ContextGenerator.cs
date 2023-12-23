using DataLayer;

namespace ServiceLayer
{
    public class ContextGenerator
    {
        private static TrainDbContext dbContext;

        private static ConnectionContext connectionContext;
        private static LocationContext locationContext;
        private static LocomotiveContext locomotiveContext;
        private static TrainCarContext trainCarContext;
        private static TrainCompositionContext trainCompositionContext;

        /*
         * return context ??= new(GetDbContext()); 
         * ^ is equivalent to:
         * return context ?? (context = new(GetDbContext());
         * ^ which is equivalent to:
         * if (context == null) context = new(GetDbContext();
         * return context;
        */

        public static ConnectionContext ConnectionContext
            => connectionContext ??= new(GetDbContext());
        public static LocationContext LocationContext
            => locationContext ??= new(GetDbContext());
        public static LocomotiveContext LocomotiveContext
            => locomotiveContext ??= new(GetDbContext());
        public static TrainCarContext TrainCarContext
            => trainCarContext ??= new(GetDbContext());
        public static TrainCompositionContext TrainCompositionContext
            => trainCompositionContext ??= new(GetDbContext());

        public static TrainDbContext GetDbContext()
        {
            if(dbContext == null)
            {
                SetDbContext();
            }
            return dbContext;
        }

        public static void SetDbContext()
        {
            dbContext?.Dispose();
            dbContext = new TrainDbContext();
        }
    }
}
