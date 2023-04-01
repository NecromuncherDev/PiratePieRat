using System;
using System.Collections.Generic;
using System.Linq;

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
            //Invoke popup event open
            popupData.OnPopupClose += OnClosePopup;
            PopupsData.Remove(popupData);
        }

        public void OnClosePopup()
        {
            //Invoke popup event close
            TryShowNextPopup();
        }
    }

    public class PPRPopupData
    {
        public int Priority;
        public PopupTypes PopupType;

        public Action OnPopupOpen;
        public Action OnPopupClose;

        public object GenericData;
    }

    public enum PopupTypes
    {

    }
}