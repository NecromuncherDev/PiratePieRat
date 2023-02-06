namespace PPR.Core
{
    public class PPRManager
    {
        public static PPRManager Instance;
        public PPREventManager EventManager;
        public PPRFactory FactoryManager;
        public PPRPoolManager PoolManager;

        public PPRManager()
        {
            if (Instance != null)
                return;
            
            Instance = this;

            EventManager = new PPREventManager();
            FactoryManager = new PPRFactory();
            PoolManager = new PPRPoolManager();
        }
    }
}
