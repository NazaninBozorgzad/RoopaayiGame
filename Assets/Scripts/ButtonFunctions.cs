using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public IAPManager iAPManager;

    #region NormalFunctioning

    public void OnShoppingButtonClicked()
    {
        SceneManager.LoadScene("Shop");
    }

    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Soccer Stadium");
    }

    public void OnExitClicked()
    {
        Application.Quit();
    }

    public void OnBackToMainMenuClicked()
    {
        SceneManager.LoadScene("Main Menu");
    }

    #endregion

    #region In-App-PurchasingFunctions
    public async void OnBuyingExtraLivesClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[0]);
        iAPManager.consumeTokens[0] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        var cosumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
        
    }

    public async void OnBuyingAmericanFootballBallClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[1]);
        iAPManager.consumeTokens[1] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        var cosumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
    }

    public async void OnBuyingPingPongBallClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[2]);
        iAPManager.consumeTokens[2] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        var cosumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
    }

    #endregion
}
