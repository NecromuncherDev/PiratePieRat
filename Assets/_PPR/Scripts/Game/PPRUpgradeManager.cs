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
        public PPRUpgradeManagerConfig UpgradeConfig; // From file. TODO: get from cloud

        // MockData
        // Load From Save Data On Device (Future)
        // Load Config From Load
        public PPRUpgradeManager()
        {
            PPRManager.Instance.ConfigManager.GetConfigAsync<PPRUpgradeManagerConfig>("upgrade_config", delegate (PPRUpgradeManagerConfig config)
            {
                UpgradeConfig = config;
            });

            PlayerUpgradeInventoryData = new PPRPlayerUpgradeInventoryData
            {
                Upgradeables = new List<PPRUpgradeableData>(){new PPRUpgradeableData
                    {
                        UpgradeableTypeID = UpgradeableTypeIDs.RatPowerUpgrade,
                        CurrentLevel = 0
                    }
                }
            };
        }

        public void UpgradeItemByID(UpgradeableTypeIDs typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);

            if (upgradeable != null)
            {
                // TODO: Config + Reduce score
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
            PPRUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
            return upgradeableConfig;
        }

        public int GetPowerByIDAndLevel(UpgradeableTypeIDs typeID, int level)
        {
            var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
            var power = upgradeableConfig.UpgradeableLevelData[level].Power;

            return power;
        }

        public PPRUpgradeableData GetUpgradeableByID(UpgradeableTypeIDs typeID)
        {
            var upgradeableData = PlayerUpgradeInventoryData.Upgradeables.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
            return upgradeableData;
        }
    }

    // Per player-owned Item
    [Serializable]
    public class PPRUpgradeableData
    {
        public UpgradeableTypeIDs UpgradeableTypeID;
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
        public UpgradeableTypeIDs UpgradeableTypeID;
        public List<PPRUpgradeableLevelData> UpgradeableLevelData;
    }

    // All config for Upgradeable
    [Serializable]
    public class PPRUpgradeManagerConfig
    {
        public List<PPRUpgradeableConfig> UpgradeableConfigs = new();
    }

    // All player saved data
    [Serializable]
    public class PPRPlayerUpgradeInventoryData : IPPRSaveData
    {
        public List<PPRUpgradeableData> Upgradeables = new();
    }

    [Serializable]
    public enum UpgradeableTypeIDs
    { 
        RatPowerUpgrade = 1,
    }
}