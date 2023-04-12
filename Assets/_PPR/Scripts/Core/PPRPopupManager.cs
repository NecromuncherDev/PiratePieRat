﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PPR.Core
{
    public class PPRPopupManager
    {
        public List<PPRPopupData> PopupsData = new();
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
        public Canvas Canvas;

        public int Priority;
        public PopupTypes PopupType;

        public Action OnPopupOpen;
        public Action OnPopupClose;

        public object GenericData;
    }

    public enum PopupTypes
    {
        WelcomePopup,
        Store,
        UpgradePopup,
    }
}