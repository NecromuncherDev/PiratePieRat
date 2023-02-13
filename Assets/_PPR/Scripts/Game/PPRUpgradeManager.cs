using System;
using System.Linq;
using System.Collections.Generic;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeManager
    {
        public PPRPlayerUpgradeInventoryData PlayerUpgradeInventoryData; // Player saved data
        public PPRUpgradeManagerConfig UpgradeConfig; // From cloud

        public void UpgradeItemByID(UpgradeableTypeIDs typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);

            if (upgradeable != null)
            {
                var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
                PPRUpgradeableLevelData levelData = upgradeableConfig.UpgradeableLevelData[upgradeable.CurrentLevel + 1]; // Get next level of item
                int amountToReduce = levelData.CurrencyCost;
                CurrencyTags currencyType = levelData.CurrencyTag;

                if (PPRGameLogic.Instance.CurrencyManager.TryUseCurrency(currencyType, amountToReduce))
                {
                    upgradeable.CurrentLevel++;
                    PPRManager.Instance.EventManager.InvokeEvent(PPREvents.item_upgraded, typeID);
                }
                else
                {
                    Debug.LogError($"UpgradeItemByID: Not enough currency of type \"{currencyType}\" to upgrade item of type \"{typeID}\".");
                }
            }
        }
        
        public PPRUpgradeableConfig GetPprUpgradeableConfigByID(UpgradeableTypeIDs typeID)
        {
            PPRUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradeable => upgradeable.UpgradeableID == typeID);
            return upgradeableConfig;
        }

        public PPRUpgradeableData GetUpgradeableByID(UpgradeableTypeIDs typeID)
        {
            var upgradeableData = PlayerUpgradeInventoryData.Upgradeables.FirstOrDefault(upgradeable => upgradeable.UpgradeableID == typeID);
            return upgradeableData;
        }
    }

    // Per player-owned Item
    [Serializable]
    public class PPRUpgradeableData
    {
        public UpgradeableTypeIDs UpgradeableID;
        public int CurrentLevel;
    }

    // Per Level in Item config
    [Serializable]
    public class PPRUpgradeableLevelData
    {
        public int Level;
        public int CurrencyCost;
        public CurrencyTags CurrencyTag;
        public string ArtItem;
        public int Power;
    }

    // Per item config
    [Serializable]
    public class PPRUpgradeableConfig
    {
        public UpgradeableTypeIDs UpgradeableID;
        public List<PPRUpgradeableLevelData> UpgradeableLevelData;
    }

    // All config for Upgradeable
    [Serializable]
    public class PPRUpgradeManagerConfig
    {
        public List<PPRUpgradeableConfig> UpgradeableConfigs;
    }

    // All player saved data
    [Serializable]
    public class PPRPlayerUpgradeInventoryData
    {
        public List<PPRUpgradeableData> Upgradeables = new();
    }

    [Serializable]
    public enum UpgradeableTypeIDs
    { 
        item_0 = 0,
        item_1 = 1,
    }
}