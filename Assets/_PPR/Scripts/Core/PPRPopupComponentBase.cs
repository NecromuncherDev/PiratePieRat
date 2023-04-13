using System;
using System.Collections.Generic;
using UnityEngine;

namespace PPR.Core
{
    public class PPRPopupComponentBase : PPRMonoBehaviour
    {
        protected PPRPopupData popupData;

        public virtual void Init(PPRPopupData data)
        {
            //var rect = GetComponent<RectTransform>();
            //rect.offsetMax = Vector2.zero;
            //rect.offsetMin = Vector2.zero;

            popupData = data;
            OnOpenPopup();
        }

        protected virtual void OnOpenPopup()
        {
            var data = new Dictionary<PPRDataKeys, object>();
            data.Add(PPRDataKeys.popup_type, popupData.PopupType);
            Manager.AnalyticsManager.ReportEvent(PPREvents.on_popup_open, data);

            popupData.OnPopupOpen?.Invoke();
        }

        public virtual void ClosePopup()
        {
            OnClosePopup();
        }

        protected virtual void OnClosePopup()
        {
            var data = new Dictionary<PPRDataKeys, object>();
            data.Add(PPRDataKeys.popup_type, popupData.PopupType);
            Manager.AnalyticsManager.ReportEvent(PPREvents.on_popup_close, data);

            popupData.OnPopupClose?.Invoke();

            Destroy(gameObject);
        }
    }
}