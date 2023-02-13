using PPR.Core;
using System.Collections.Generic;

namespace PPR.Game
{
    public class PPRCurrencyManager
    {
        public PPRPlayerCurrencyData PlayerCurrencyData;

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
            PPRManager.Instance.EventManager.InvokeEvent(PPREvents.on_currency_set, (tag, amount));
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
    }

    public class PPRPlayerCurrencyData
    {
        public Dictionary<CurrencyTags, int> CurrencyByTag = new();
    }

    public enum CurrencyTags
    {
        Crew = 0,
        Pies = 1,
        Cheese = 2,
    }
}