using PPR.Core;
using System;
using System.Collections.Generic;

namespace PPR.Game
{
    public class PPRCurrencyManager
    {
        public PPRPlayerCurrencyData PlayerCurrencyData = new();

        public PPRCurrencyManager()
        {
            PPRManager.Instance.EventManager.AddListener(PPREvents.currency_collected, OnCurrencyCollected);

            PPRManager.Instance.SaveManager.Load<PPRPlayerCurrencyData>(delegate (PPRPlayerCurrencyData data)
            {
                PlayerCurrencyData = data ?? new PPRPlayerCurrencyData();
            });
        }

        /// <summary>
        /// Try (If manage to success return true)
        /// Wrong tag will return false
        /// Get = return
        /// Finding the currency in dictionary according to the tag
        /// Input a reference value
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="currencyOut"></param>
        /// <returns></returns>
        public bool TryGetCurrencyByTag(CurrencyTags tag, ref int currencyOut)
        {
            if (PlayerCurrencyData.CurrencyByTag.TryGetValue(tag, out var currency))
            {
                currencyOut = currency;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set = Setting (Equal)
        /// Score of tag to currency input (amount)
        /// Override the amount
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="amount"></param>
        public void SetCurrencyByTag(CurrencyTags tag, int amount = 0)
        {
            PlayerCurrencyData.CurrencyByTag[tag] = amount;
            PPRManager.Instance.EventManager.InvokeEvent(PPREvents.currency_set, (tag, amount));

            PPRManager.Instance.SaveManager.Save(PlayerCurrencyData); // Saves AT LEAST once a second due to the nature of the game
        }

        public void ChangeCurrencyByTagByAmount(CurrencyTags tag, int amount)
        {
            int changeAmount = PlayerCurrencyData.CurrencyByTag.ContainsKey(tag) ?
                               PlayerCurrencyData.CurrencyByTag[tag] + amount :
                               amount;

            SetCurrencyByTag(tag, changeAmount);
        }

        public bool TryUseCurrency(CurrencyTags tag, int amountToReduce)
        {
            int currency = 0;
            bool hasType = TryGetCurrencyByTag(tag, ref currency);
            bool hasEnough = false;

            if (hasType)
            {
                hasEnough = amountToReduce <= currency;
            }

            if (hasEnough)
            {
                ChangeCurrencyByTagByAmount(tag, -amountToReduce);
            }

            return hasEnough;
        }

        private void OnCurrencyCollected(object obj)
        {
            var data = ((CurrencyTags, int))obj;
            ChangeCurrencyByTagByAmount(data.Item1, data.Item2);
        }

        ~PPRCurrencyManager()
        {
            PPRManager.Instance.EventManager.RemoveListener(PPREvents.currency_collected, OnCurrencyCollected);
        }
    }

    [Serializable]
    public class PPRPlayerCurrencyData : IPPRSaveData
    {
        public Dictionary<CurrencyTags, int> CurrencyByTag = new();
    }

    [Serializable]
    public enum CurrencyTags
    {
        NA = 0,
        Crew = 1,
        Pies = 2,
        Cheese = 3,
    }
}