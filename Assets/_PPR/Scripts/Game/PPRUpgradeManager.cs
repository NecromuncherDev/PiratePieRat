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

            PPRManager.Instance.SaveManager.Load<PPRPlayerUpgradeData>(delegate (PPRPlayerUpgradeData data)
            {
                PlayerUpgradeData = data ?? new PPRPlayerUpgradeData();
            });

            if (PlayerUpgradeData.Upgradeables.Count == 0)
            {
                foreach (UpgradeableTypeIDs id in Enum.GetValues(typeof(UpgradeableTypeIDs))) // Change to be configurable in firebase
                {
                    PlayerUpgradeData.Upgradeables.Add(new PPRUpgradeableData
                    {
                        UpgradeableTypeID = id,
                        CurrentLevel = 0
                    });
                }
            }
        }

        public bool UpgradeItemByID(UpgradeableTypeIDs typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);
            
            if (upgradeable == null)
                return false;
            
            Dictionary<CurrencyTags, int> costs = GetUpgradeCostsByID(typeID);

            if (costs == null)
            {
                Debug.LogError($"UpgradeItemByID: Cannot upgrade \"{typeID}\" beyond current level ({upgradeable.CurrentLevel}).");
                return false;
            }

            if (PPRGameLogic.Instance.CurrencyManager.TryUseCurrency(costs, true))
            {
                upgradeable.CurrentLevel++;
                PPRManager.Instance.EventManager.InvokeEvent(PPREvents.item_upgraded, typeID);
                Debug.Log($"Upgraded \"{typeID}\" to level {upgradeable.CurrentLevel}!");
                NotifyUpgraded(typeID);
                PPRManager.Instance.SaveManager.Save(PlayerUpgradeData);

                return true;
            }
            else
            {
                Debug.LogError($"UpgradeItemByID: Not enough currency to upgrade item of type \"{typeID}\".");
                return false;
            }
        }

        public Dictionary<CurrencyTags, int> GetUpgradeCostsByID(UpgradeableTypeIDs typeID)
        {
            var upgradeable = GetUpgradeableByID(typeID);
            var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
            int upgradeableMaxLevel = upgradeableConfig.UpgradeableLevelData[upgradeableConfig.UpgradeableLevelData.Count - 1].Level;

            if (upgradeable.CurrentLevel < upgradeableMaxLevel)
            {
                PPRUpgradeableLevelData levelData = upgradeableConfig.UpgradeableLevelData[upgradeable.CurrentLevel + 1]; // Get next level of item
                Dictionary<CurrencyTags, int> costs = levelData.CurrencyCost;
                return costs;
            }
            else
            {
                Debug.LogError($"GetUpgradeCostsByID: Cannot get \"{typeID}\" costs beyond current level ({upgradeable.CurrentLevel}).");
                return null;
            }
        }

        public PPRUpgradeableConfig GetPprUpgradeableConfigByID(UpgradeableTypeIDs typeID)
        {
            PPRUpgradeableConfig upgradeableConfig = UpgradeConfig.UpgradeableConfigs.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
            return upgradeableConfig;
        }

        public float GetCurrentValueByID(UpgradeableTypeIDs typeID)
        {
            int level = GetUpgradeableByID(typeID).CurrentLevel;
            return GetValueByIDAndLevel(typeID, level);
        }

        public float GetValueByIDAndLevel(UpgradeableTypeIDs typeID, int level)
        {
            var upgradeableConfig = GetPprUpgradeableConfigByID(typeID);
            var ugradeableValue = upgradeableConfig.UpgradeableLevelData[level].Value;

            return ugradeableValue;
        }

        public PPRUpgradeableData GetUpgradeableByID(UpgradeableTypeIDs typeID)
        {
            var upgradeableData = PlayerUpgradeData.Upgradeables.FirstOrDefault(upgradeable => upgradeable.UpgradeableTypeID == typeID);
            return upgradeableData;
        }

        private void NotifyUpgraded(UpgradeableTypeIDs typeID)
        {
            PPREvents upgradeableEvent = PPREvents.empty_event;

            switch (typeID)
            {
                case UpgradeableTypeIDs.MovementSpeed:
                    upgradeableEvent = PPREvents.upgraded_movement_speed;
                    break;
                case UpgradeableTypeIDs.GatheringSpeed:
                    upgradeableEvent = PPREvents.upgraded_gathering_speed;
                    break;
                case UpgradeableTypeIDs.GatheringRange:
                    upgradeableEvent = PPREvents.upgraded_gathering_range;
                    break;
                case UpgradeableTypeIDs.RadarRange:
                    upgradeableEvent = PPREvents.upgraded_radar_range;
                    break;
                default:
                    break;
            }

            if (upgradeableEvent != PPREvents.empty_event)
            {
                PPRManager.Instance.EventManager.InvokeEvent(upgradeableEvent);
            }
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
        public List<PPRUpgradeableData> Upgradeables = new(); // Make sure this fits the data model
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