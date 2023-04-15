using TMPro;
using UnityEngine;
using PPR.Core;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PPR.Game
{
    public class PPRUpgradeShopCostTile : PPRMonoBehaviour
    {
        public CurrencyTags costCurrency;

        [Header("Init")]
        [SerializeField] private Image costImage;
        [SerializeField] private TMP_Text costAmount;

        [Header("Images")]
        [SerializeField] private List<CurrencyImage> currencyImages = new();
        [Serializable]
        public struct CurrencyImage
        {
            [SerializeField] public CurrencyTags tag;
            [SerializeField] public Sprite image;
        }

        public void Init(CurrencyTags currency, int amount)
        {
            costCurrency = currency;
            costImage.sprite = currencyImages.First(x => x.tag == costCurrency).image;
            costAmount.text = amount.ToString("N0");
        }
    }
}