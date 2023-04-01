using System;
using PPR.Core;

namespace PPR.Game
{
    public class PPRGameLogic : IPPRBaseManager
    {
        public static PPRGameLogic Instance;

        public PPRCurrencyManager CurrencyManager;
        public PPRUpgradeManager UpgradeManager;
        public PPRStoreManager StoreManager;
        public PPRMonoManager MonoManager;

        private PPRRatBasedPieGenerator ratPieGenerator;
        
        public PPRGameLogic()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        public void LoadManager(Action onComplete)
        {
            CurrencyManager = new();
            UpgradeManager = new();
            StoreManager = new();
            MonoManager = new();
            ratPieGenerator = new();

            onComplete.Invoke();
        }
    }
}