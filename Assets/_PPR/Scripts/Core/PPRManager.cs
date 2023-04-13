using Firebase.Extensions;
using System;

namespace PPR.Core
{
    public class PPRManager : IPPRBaseManager
    {
        public static PPRManager Instance;

        public PPRAdManager AdManager;
        public PPRAnalyticsManager AnalyticsManager;
        public PPRConfigManager ConfigManager;
        public PPRCrashManager CrashManager;
        public PPREventManager EventManager;
        public PPRFactory FactoryManager;
        public PPRInAppPurchace PurchaseManager;
        public PPRInfoManager InfoManager;
        public PPRPoolManager PoolManager;
        public PPRPopupManager PopupManager;
        public PPRSaveManager SaveManager;
        public PPRTimeManager TimerManager;

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
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
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

            PPRDebug.Log($"Before ConfigManager");
            ConfigManager = new PPRConfigManager(delegate
            {
                EventManager = new PPREventManager();
                PPRDebug.Log($"After EventsManager");

                AnalyticsManager = new PPRAnalyticsManager();
                PPRDebug.Log($"After AnalyticsManager");

                FactoryManager = new PPRFactory();
                PPRDebug.Log($"After FactoryManager");

                SaveManager = new PPRSaveManager();
                PPRDebug.Log($"After SaveManager");

                PopupManager = new PPRPopupManager();
                PPRDebug.Log($"After PopupManager");

                PoolManager = new PPRPoolManager();
                PPRDebug.Log($"After PoolManager");

                TimerManager = new PPRTimeManager();
                PPRDebug.Log($"After TimeManager");

                PurchaseManager = new PPRInAppPurchace();
                PPRDebug.Log($"After PurchaseManager");

                AdManager = new PPRAdManager();
                PPRDebug.Log($"After AdManager");

                PPRDebug.Log($"Before InfoManager");
                InfoManager = new PPRInfoManager(delegate
                {
                    OnInitAction.Invoke();
                });
            });
        }
    }
    public interface IPPRBaseManager
    {
        public void LoadManager(Action onComplete);
    }
}
