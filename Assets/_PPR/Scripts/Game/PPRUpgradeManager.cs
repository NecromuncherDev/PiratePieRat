using System;
using System.Linq;
using System.Collections.Generic;
using PPR.Core;
using UnityEngine;

namespace PPR.Game
{
    public class PPRUpgradeManager
    {
        public PPRPlayerUpgradeData PlayerUpgradeData;
        public PPRUpgradeManagerConfig UpgradeConfig;

        public PPRUpgradeManager()
        {
            PPRManager.Instance.ConfigManager.GetConfigAsync<PPRUpgradeManagerConfig>("upgrade_config", delegate (PPRUpgradeManagerConfig config)
            {
                UpgradeConfig = config;
            });

            PlayerUpgradeData = new PPRPlayerUpgradeData();

            PlayerUpgradeData.Upgradeables = new List<PPRUpgradeableData>();

            foreach (UpgradeableTypeIDs id in Enum.GetValues(typeof(UpgradeableTypeIDs)))
            {
                PlayerUpgradeData.Upgradeables.Add(new PPRUpgradeableData
                {
                    UpgradeableTypeID = id,
                    CurrentLevel = 0
                });
            }
        }

        public void UpgradeItemByID(UpgradeableTypeIDs typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);

            if (upgradeable != null)
            {
                var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
                PPRUpgradeableLevelData levelData = upgradeableConfig.UpgradeableLevelData[upgradeable.CurrentLevel + 1]; // Get next level of item

                Dictionary<CurrencyTags, int> costs = levelData.CurrencyCost;

                if (PPRGameLogic.Instance.CurrencyManager.TryUseCurrency(costs, true))
                {
                    upgradeable.CurrentLevel++;
                    PPRManager.Instance.EventManager.InvokeEvent(PPREvents.item_upgraded, typeID);
                    Debug.Log($"Upgraded \"{typeID}\" to level {upgradeable.CurrentLevel}!");
                }
                else
                {
                    Debug.LogError($"UpgradeItemByID: Not enough currency to upgrade item of type \"{typeID}\".");
                }

            }
        }
        
        public PPRUpgradeableConfig GetPprUpgradeableConfigByID(UpgradeableTypeIDs typeID)
        {
            PPRUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
            return upgradeableConfig;
        }

        public float GetPowerByIDAndLevel(UpgradeableTypeIDs typeID, int level)
        {
            var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
            var power = upgradeableConfig.UpgradeableLevelData[level].Value;

            return power;
        }

        public PPRUpgradeableData GetUpgradeableByID(UpgradeableTypeIDs typeID)
        {
            var upgradeableData = PlayerUpgradeData.Upgradeables.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
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
        public Dictionary<CurrencyTags, int> CurrencyCost = new();
        public string ArtItem;
        public float Value;
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
    public class PPRPlayerUpgradeData : IPPRSaveData
    {
        public List<PPRUpgradeableData> Upgradeables = new();
    }

    [Serializable]
    public enum UpgradeableTypeIDs
    {
        MovementSpeed = 1,
        GatheringSpeed = 2,
        GatheringRange = 3,
        RadarRange = 4,
    }
}