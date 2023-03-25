using PPR.Core;
using System.Collections.Generic;
using System.Linq;

namespace PPR.Game
{
    public class PPRStoreManager
    {
        public PPRStoresConfigData StoresConfigData;

        public bool TryBuyProduct(string sku, string storeID)
        {
            PPRManager.Instance.PurchaseManager.Purchace(sku, (bool isSuccess) => 
            {
                if (isSuccess)
                {
                    var product = GetStoreByStoreID(storeID).StoreProducts.First(x => x.SKU == sku);
                }
            });

            return false;
        }

        public PPRStoreData GetStoreByStoreID(string storeID)
        {
            var storeConfigData = StoresConfigData.StoreDatas.FirstOrDefault(x => x.StoreID == storeID);
            return storeConfigData;
        }

        private void RedeemBundle(PPRStoreBundle[] productStoreBundle)
        {
            foreach (var bundle in productStoreBundle)
            {
                PPRGameLogic.Instance.CurrencyManager.ChangeCurrencyByTagByAmount(bundle.CurrencyTag, bundle.CurrencyAmount);
            }
        }
    }

    public class PPRStoresConfigData
    {
        public List<PPRStoreData> StoreDatas = new();
    }

    public class PPRStoreData
    {
        public string Title;
        public string StoreID;
        public List<PPRStoreProduct> StoreProducts = new();
    }

    public class PPRStoreProduct
    {
        public string SKU;
        public string Art;
        public string ProductDescription;
        public PPRStoreBundle[] StoreBundles;
    }

    public class PPRStoreBundle
    {
        public CurrencyTags CurrencyTag;
        public int CurrencyAmount;
    }
}