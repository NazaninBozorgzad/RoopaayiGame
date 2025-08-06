using UnityEngine;
using Bazaar.Poolakey;
using Bazaar.Data;
using TMPro;
using System.Threading.Tasks;
using Bazaar.Poolakey.Data;
public class IAPManager : MonoBehaviour
{
    private Payment payment;
    [SerializeField] private string appKey;
    public string[] products = new string[3];
    public string[] consumeTokens = new string[3];
    public bool isInitilaized;
    public TextMeshProUGUI resultTxt;

    private async void Start()
    {
        var wasSuccessful = await Init();
        if (!wasSuccessful)
        {
            resultTxt.text = "Initialization failed.";
            resultTxt.color = Color.red;
            return;
        }
        isInitilaized = true;
    }

    public async Task<bool> Init()
    {
        SecurityCheck securityCheck = SecurityCheck.Enable(appKey);
        PaymentConfiguration paymentConfiguration = new PaymentConfiguration(securityCheck);
        payment = new Payment(paymentConfiguration);
        var result = await payment.Connect();
        resultTxt.text = result.message;
        return result.status == Status.Success;
    }

    public async Task<Result<PurchaseInfo>> Purchace(string productID)
    {
        var result = await payment.Purchase(productID);
        resultTxt.text = result.message;
        return result;
    }

    public async Task<Result<bool>> Consume(string purchaseToken)
    {
        var result = await payment.Consume(purchaseToken);
        resultTxt.text = result.message;
        return result;
    }

    private void OnApplicationQuit()
    {
        payment.Disconnect();
    }
}
