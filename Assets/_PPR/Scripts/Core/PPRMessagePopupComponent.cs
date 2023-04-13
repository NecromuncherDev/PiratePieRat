using TMPro;
using UnityEngine;

namespace PPR.Core
{
    public class PPRMessagePopupComponent : PPRTweenPopupComponent
    {
        [Header("Message")]
        [SerializeField] private TMP_Text message;

        protected override void OnOpenPopup()
        {
            message.SetText((string) popupData.GenericData);
            base.OnOpenPopup();
        }


    }
}