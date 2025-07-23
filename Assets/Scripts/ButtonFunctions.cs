using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public IAPManager iAPManager;
    public Button amFootballballApplyButton;
    public Button PingPongApplyButton;

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
    Bazaar.Data.Result<bool> extraLivesConsumeResult;
    public async void OnBuyingExtraLivesClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[0]);
        iAPManager.consumeTokens[0] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        extraLivesConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);

    }
    Bazaar.Data.Result<bool> amFootballConsumeResult;
    public async void OnBuyingAmericanFootballBallClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[1]);
        iAPManager.consumeTokens[1] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        amFootballConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
        if (amFootballConsumeResult.data) amFootballballApplyButton.interactable = true;
    }

    Bazaar.Data.Result<bool> pingpongBallConsumeResult;
    public async void OnBuyingPingPongBallClicked()
    {
        //Buy it first
        var purchaseResult = await iAPManager.Purchace(iAPManager.products[2]);
        iAPManager.consumeTokens[2] = purchaseResult.data.purchaseToken;
        //Then, consume it!
        pingpongBallConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
        if (pingpongBallConsumeResult.data) PingPongApplyButton.interactable = true;
    }

    public void OnApplyingAmFootballBallClicked()
    {
        PlayerPrefs.SetString("Ball Type", "American Football");
    }

    public void OnApplyingPingPongBallClicked()
    {
        PlayerPrefs.SetString("Ball Type", "Ping Pong");
    }

    public void OnResetBallClicked()
    {
        PlayerPrefs.SetString("Ball Type", "Default");
    }

    #endregion
}
