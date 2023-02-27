using System;

namespace PPR.Core
{
    public class PPRManager : IPPRBaseManager
    {
        public static PPRManager Instance;

        public PPREventManager EventManager;
        public PPRFactory FactoryManager;
        public PPRPoolManager PoolManager;
        public PPRConfigManager ConfigManager;
        public PPRSaveManager SaveManager;

        public PPRManager()
        {
            if (Instance != null)
                return;
            
            Instance = this;
        }

        public void LoadManager(Action onComplete)
        {
            EventManager = new PPREventManager();
            FactoryManager = new PPRFactory();
            PoolManager = new PPRPoolManager();
            ConfigManager = new PPRConfigManager();
            SaveManager = new PPRSaveManager();

            onComplete.Invoke();
        }
    }

    public interface IPPRBaseManager
    {
        public void LoadManager(Action onComplete);
    }
}
