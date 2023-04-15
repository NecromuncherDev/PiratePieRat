using System;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeStoreComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private Transform storeParent;
        [SerializeField] private PPRUpgradeStoreItemComponent storeItemPrefab;

        private void Start()
        {
            foreach (UpgradeableTypeIDs id in Enum.GetValues(typeof(UpgradeableTypeIDs)))
            {
                Manager.FactoryManager.CreateAsync(storeItemPrefab, Vector3.zero, (PPRUpgradeStoreItemComponent storeItem) => 
                {
                    storeItem.transform.SetParent(storeParent, false);
                    var costs = GameLogic.UpgradeManager.GetUpgradeCostsByID(id);
                    storeItem.Init(id, costs);
                });
            }
        }
    }
}