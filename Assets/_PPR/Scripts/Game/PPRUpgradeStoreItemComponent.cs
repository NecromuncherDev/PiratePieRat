﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using PPR.Core;
using UnityEngine.UI;
using System;
using System.Linq;

namespace PPR.Game
{
    public class PPRUpgradeStoreItemComponent : PPRLogicMonoBehaviour
    {
        [SerializeField] private TMP_Text title;
        [SerializeField] private PPRUpgradeShopCostTile costTilePrefab;
        [SerializeField] private LayoutGroup costParent;

        private UpgradeableTypeIDs type;
        private List<PPRUpgradeShopCostTile> costTiles = new();
        private Dictionary<CurrencyTags, int> costs;

        public void Init(UpgradeableTypeIDs upgradeableType, Dictionary<CurrencyTags, int> upgradeableCosts)
        {
            type = upgradeableType;
            title.text = Regex.Replace(type.ToString(), "(\\B[A-Z])", " $1");

            costs = upgradeableCosts;
            foreach (var cost in costs)
            {
                Manager.FactoryManager.CreateAsync<PPRUpgradeShopCostTile>(costTilePrefab, Vector3.zero, (PPRUpgradeShopCostTile costTile) =>
                {
                    costTile.transform.SetParent(costParent.transform, false);
                    costTile.Init(cost.Key, cost.Value);
                    costTiles.Add(costTile);
                });
            }
        }

        public void TryBuyUpgrade()
        {
            if (GameLogic.UpgradeManager.UpgradeItemByID(type))
            {
                UpdateUpgradeableCosts();
            }
        }

        private void UpdateUpgradeableCosts()
        {
            costs = GameLogic.UpgradeManager.GetUpgradeCostsByID(type);
            foreach (var tile in costTiles)
            {
                tile.Init(tile.costCurrency, costs[tile.costCurrency]);
            }
        }
    }
}