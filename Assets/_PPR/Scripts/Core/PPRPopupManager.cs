using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace PPR.Core
{
    public class PPRPopupManager
    {
        public List<PPRPopupData> PopupsData = new();
        public Canvas PopupCanvas;

        public PPRPopupManager()
        {
            CreateCanvas();

            PPRManager.Instance.ConfigManager.GetConfigAsync<PPRPopupMessagesConfig>("popup_messages_config", OnPopupMessagesLoaded);
        }

        private void OnPopupMessagesLoaded(PPRPopupMessagesConfig popupMessagesConfig)
        {
            PPRPopupData.Init(popupMessagesConfig);
        }

        private void CreateCanvas()
        {
            PPRManager.Instance.FactoryManager.CreateAsync<Canvas>("Popup Canvas", Vector3.zero, (Canvas canvas) =>
            {
                PopupCanvas = canvas;
                Object.DontDestroyOnLoad(PopupCanvas);
            });
        }

        public void AddPopupToQueue(PPRPopupData popupData)
        {
            PopupsData.Add(popupData);
            TryShowNextPopup();
        }

        public void TryShowNextPopup()
        {
            if (PopupsData.Count <= 0)
            {
                return;
            }

            SortPopups();
            OpenPopup(PopupsData[0]);
        }

        public void SortPopups()
        {
            PopupsData = PopupsData.OrderBy(x => x.Priority).ToList();
        }

        public void OpenPopup(PPRPopupData popupData)
        {
            popupData.OnPopupClose += OnClosePopup;
            PopupsData.Remove(popupData);

            // Instantiate
            PPRManager.Instance.FactoryManager.CreateAsync<PPRPopupComponentBase>(popupData.PopupType.ToString(), 
                                                                                  Vector3.zero, 
                                                                                  (PPRPopupComponentBase popupComponent) =>
                                                                                  {
                                                                                      popupComponent.transform.SetParent(PopupCanvas.transform, false);
                                                                                      popupComponent.Init(popupData);
                                                                                  });
        }

        public void OnClosePopup()
        {
            TryShowNextPopup();
        }
    }

    public class PPRPopupData
    {
        // public Canvas Canvas;

        public int Priority;
        public PopupTypes PopupType;

        public Action OnPopupOpen;
        public Action OnPopupClose;

        public object GenericData;

        public static PPRPopupData WelcomeMessage = new()
        {
            Priority = 0,
            PopupType = PopupTypes.WelcomePopup,
        };

        public static PPRPopupData UpgradeStore = new()
        {
            Priority = 0,
            PopupType = PopupTypes.UpgradePopup,
        };

        public static void Init(PPRPopupMessagesConfig config)
        {
            WelcomeMessage.GenericData = config.WelcomeMessages.GetRandomFromArray();
            UpgradeStore.GenericData = Resources.Load("Upgrade Store Popup Scroll View");
            //UpgradeStore
        }
    }


    [Serializable]
    public class PPRPopupMessagesConfig
    {
        [JsonProperty("welcome_messages")]
        public string[] WelcomeMessages;

        [JsonProperty("welcome_back_messages")]
        public string[] WelcomeBackMessages;
    }

    public enum PopupTypes
    {
        WelcomePopup,
        Store,
        UpgradePopup,
    }
}