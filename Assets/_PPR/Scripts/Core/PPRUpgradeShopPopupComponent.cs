using UnityEngine;

namespace PPR.Core
{
    public class PPRUpgradeShopPopupComponent : PPRTweenPopupComponent
    {
        [SerializeField] private Transform storeWindowParent;
        public static bool IsUpgradeStoreOpen { get; private set; } = false;

        public override void Init(PPRPopupData data)
        {
            IsUpgradeStoreOpen = true;
            base.Init(data);

            Manager.FactoryManager.CreateAsync<GameObject>((GameObject)data.GenericData, Vector3.zero, (GameObject obj) => 
            {
                obj.transform.SetParent(storeWindowParent, false);
            });
        }

        public override void ClosePopup()
        {
            base.ClosePopup();
            IsUpgradeStoreOpen = false;
        }
    }
}