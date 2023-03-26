using System;
using System.Collections.Generic;
using UnityEngine.Purchasing;

namespace PPR.Core
{

    public class PPRInAppPurchace : IStoreListener
    {
        private IStoreController storeController;
        private IExtensionProvider extensionProvider;

        private Action<bool> purchaseCompleteAction;

        public PPRInAppPurchace()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            UnityPurchasing.Initialize(this, builder);
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            storeController = controller;
            extensionProvider = extensions;
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            PPRManager.Instance.CrashManager.LogExceptionHandling(error.ToString());
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            PPRManager.Instance.CrashManager.LogExceptionHandling(error.ToString() + "   " + message);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            
            PPRManager.Instance.CrashManager.LogBreadcrumb(product.receipt);
            PPRManager.Instance.CrashManager.LogExceptionHandling(failureReason.ToString());
            
            purchaseCompleteAction?.Invoke(false);
            purchaseCompleteAction = null;
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            var receipt = purchaseEvent.purchasedProduct.receipt;
            PPRManager.Instance.AnalyticsManager.ReportEvent(PPREvents.purchase_complete, new Dictionary<PPRDataKeys, object>()
            {
                {PPRDataKeys.product_id, purchaseEvent.purchasedProduct.definition.id},
                {PPRDataKeys.product_receipt, receipt}
            });

            purchaseCompleteAction?.Invoke(true);
            purchaseCompleteAction = null;

            return PurchaseProcessingResult.Complete;
        }

        public void Purchace(string productID, Action<bool> onPurchaseComplete)
        {
            purchaseCompleteAction = onPurchaseComplete;
            storeController.InitiatePurchase(productID);
        }
    }
}