using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;

    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    public string extraLifeProductId = "extra_life";
    private int purchasedLives;

    void Start()
    {
        instance = this;
        InitializePurchasing();
        purchasedLives = PlayerPrefs.GetInt("Purchased Lives");
    }

    public void InitializePurchasing()
    {
        if (IsInitialized()) return;

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.AddProduct(extraLifeProductId, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return storeController != null && storeExtensionProvider != null;
    }

    public void BuyExtraLife()
    {
        if (IsInitialized())
        {
            Product product = storeController.products.WithID(extraLifeProductId);

            if (product != null && product.availableToPurchase)
            {
                storeController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("❌ Product not available for purchase.");
            }
        }
        else
        {
            Debug.Log("❌ IAP not initialized.");
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == extraLifeProductId)
        {
            Debug.Log("✅ Extra Life Purchased!");
            purchasedLives += 1;
            PlayerPrefs.SetInt("Purchased Lives", purchasedLives); // جان اضافه کن
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log($"❌ Purchase Failed: {product.definition.id} - {failureReason}");
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("✅ IAP Initialized");
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError($"❌ IAP Init Failed (old): {error}");
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError($"❌ IAP Init Failed (new): {error} - {message}");
    }
}
