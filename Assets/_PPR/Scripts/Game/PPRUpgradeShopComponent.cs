using System;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeShopComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private Transform storeParent;
        [SerializeField] private PPRUpgradeShopItemComponent storeItemPrefab;

        private void Start()
        {
            foreach (UpgradeableTypeIDs id in Enum.GetValues(typeof(UpgradeableTypeIDs)))
            {
                Manager.FactoryManager.CreateAsync(storeItemPrefab, Vector3.zero, (PPRUpgradeShopItemComponent storeItem) => 
                {
                    var costs = GameLogic.UpgradeManager.GetUpgradeCostsByID(id);
                    if (costs != null)
                    {
                        storeItem.transform.SetParent(storeParent, false);
                        storeItem.Init(id, costs);
                    }
                    else
                    {
                        Debug.Log($"Not generating store tile for \"{id}\" - at max level or does not exist.");
                    }
                });
            }
        }
    }
}