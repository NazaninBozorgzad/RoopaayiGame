using TMPro;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class IAPManager : MonoBehaviour, IStoreListener
{
    public static IAPManager instance;
    public TextMeshPro feedback;

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
                feedback.text = "Product is not available for purchasing";
                feedback.color = Color.red;
            }
        }
        else
        {
            feedback.text = "IAP hasn't initialized";
            feedback.color = Color.red;
        }
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (args.purchasedProduct.definition.id == extraLifeProductId)
        {
            feedback.text = "ExtraLives purchased!";
            feedback.color = Color.green;
            purchasedLives += 1;
            PlayerPrefs.SetInt("Purchased Lives", purchasedLives); // جان اضافه کن
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        feedback.text = $"Purchase failed: {product.definition.id} because {failureReason}";
        feedback.color = Color.red;
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        feedback.text = $"IAP initilization failed: {error}, please try again later.";
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        feedback.text = $"IAP initilization failed: {error} - {message}, please try again later.";
    }
}
