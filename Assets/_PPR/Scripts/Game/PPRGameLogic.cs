﻿using System;
using PPR.Core;

namespace PPR.Game
{
    public class PPRGameLogic : IPPRBaseManager
    {
        public static PPRGameLogic Instance;

        public PPRCurrencyManager CurrencyManager;
        public PPRUpgradeManager UpgradeManager;

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
            ratPieGenerator = new();

            onComplete.Invoke();
        }
    }
}