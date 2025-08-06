using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    public IAPManager iAPManager;
    public Button amFootballballApplyButton;
    public Button pingPongBallApplyButton;
    public Button amFootballBallBuyButton;
    public Button pingPongBallBuyButton;

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
        if (iAPManager != null)
        {
            //Buy it first
            var purchaseResult = await iAPManager.Purchace(iAPManager.products[0]);
            iAPManager.consumeTokens[0] = purchaseResult.data.purchaseToken;
            //Then, consume it!
            extraLivesConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[0]);
            if (extraLivesConsumeResult.data)
            {
                //We'll apply the new purchased life immideatly after the purchase
                int purchasedLives = PlayerPrefs.GetInt("Purchased Lives");
                PlayerPrefs.SetInt("Purchased Lives", purchasedLives + 1);
            }
        }
    }
    Bazaar.Data.Result<bool> amFootballConsumeResult;
    public async void OnBuyingAmericanFootballBallClicked()
    {
        if (iAPManager != null && amFootballballApplyButton != null && amFootballBallBuyButton != null)
        {
            //Buy it first
            var purchaseResult = await iAPManager.Purchace(iAPManager.products[1]);
            string temp = purchaseResult.data.purchaseToken;
            iAPManager.consumeTokens[1] = temp;
            //Then, consume it!
            amFootballConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[1]);
            if (amFootballConsumeResult.data)
            {
                amFootballballApplyButton.interactable = true;
                //We won't let them buy twice, unless we're scamming them
                amFootballBallBuyButton.interactable = false;
                PlayerPrefs.SetInt("AmFootballBallPurchased", 1);
            }
            else
            {
                PlayerPrefs.SetInt("AmFootballBallPurchased", 0);
            }

        }
    }

    Bazaar.Data.Result<bool> pingpongBallConsumeResult;
    public async void OnBuyingPingPongBallClicked()
    {
        if (iAPManager != null && pingPongBallApplyButton != null && pingPongBallBuyButton != null)
        {
            //Buy it first
            var purchaseResult = await iAPManager.Purchace(iAPManager.products[2]);
            iAPManager.consumeTokens[2] = purchaseResult.data.purchaseToken;
            //Then, consume it!
            pingpongBallConsumeResult = await iAPManager.Consume(iAPManager.consumeTokens[2]);
            if (pingpongBallConsumeResult.data)
            {
                //We won't let them buy twice, unless we're scamming them
                pingPongBallApplyButton.interactable = true;
                pingPongBallBuyButton.interactable = false;
                PlayerPrefs.SetInt("PingPongBallPurchased", 1);
            }
            else
            {
                PlayerPrefs.SetInt("PingPongBallPurchased", 0);
            }
        }
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

    #region UnityMessages
    private void Awake()
    {
        //Exception handling for main menu buttons and NULL iAPManager variable: There are no IAP manager and buying buttons there.
        if
        (
            iAPManager == null ||
            amFootballballApplyButton == null ||
            pingPongBallApplyButton == null ||
            amFootballBallBuyButton == null ||
            pingPongBallBuyButton == null
        )
        {
            return;
        }

        //Pre setup for buying ans applying buttons to prevent common issues afterwards.
        if (PlayerPrefs.GetInt("AmFootballBallPurchased") == 1)
        {
            amFootballBallBuyButton.interactable = false;
            amFootballballApplyButton.interactable = true;
        }
        else if (PlayerPrefs.GetInt("AmFootballBallPurchased") == 0)
        {
            amFootballballApplyButton.interactable = false;
            amFootballBallBuyButton.interactable = true;
        }

        if (PlayerPrefs.GetInt("PingPongBallPurchased") == 1)
        {
            pingPongBallBuyButton.interactable = false;
            pingPongBallApplyButton.interactable = true;
        }
        else if (PlayerPrefs.GetInt("PingPongBallPurchased") == 0)
        {
            pingPongBallApplyButton.interactable = false;
            pingPongBallBuyButton.interactable = true;
        }
    }
    #endregion
}