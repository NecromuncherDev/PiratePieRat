using Firebase.Extensions;
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
        public PPRAnalyticsManager AnalyticsManager;
        public PPRCrashManager CrashManager;
        public PPRTimeManager TimerManager;
        public PPRInAppPurchace PurchaseManager;

        public Action OnInitAction;

        public PPRManager()
        {
            if (Instance != null)
                return;

            Instance = this;
        }

        public void LoadManager(Action onComplete)
        {
            OnInitAction = onComplete;
            InitFirebase(delegate
            {
                InitManagers();
            });
        }

        public void InitFirebase(Action onComplete)
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    var app = Firebase.FirebaseApp.DefaultInstance;
                    PPRDebug.Log($"Firebase was initialized");
                    onComplete.Invoke();
                }
                else
                {
                    PPRDebug.LogException($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        private void InitManagers()
        {
            PPRDebug.Log($"InitManagers");

            CrashManager = new PPRCrashManager();
            PPRDebug.Log($"After CrashManager");

            AnalyticsManager = new PPRAnalyticsManager();
            PPRDebug.Log($"After AnalyticsManager");

            EventManager = new PPREventManager();
            PPRDebug.Log($"After HOGEventsManager");

            FactoryManager = new PPRFactory();
            PPRDebug.Log($"After HOGFactoryManager");

            PoolManager = new PPRPoolManager();
            PPRDebug.Log($"After PPRPoolManager");

            SaveManager = new PPRSaveManager();
            PPRDebug.Log($"After PPRSaveManager");

            TimerManager = new PPRTimeManager();
            PPRDebug.Log($"After TimeManager");

            PurchaseManager = new PPRInAppPurchace();
            PPRDebug.Log($"After PurchaseManager");

            PPRDebug.Log($"Before Config Manager");
            ConfigManager = new PPRConfigManager(delegate
            {
                OnInitAction.Invoke();
            });
        }
    }

    public interface IPPRBaseManager
    {
        public void LoadManager(Action onComplete);
    }
}
