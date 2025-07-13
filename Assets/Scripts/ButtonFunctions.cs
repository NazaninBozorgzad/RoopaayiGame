using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public IAPManager iAPManager;

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

    public void OnBuyingExtraLivesClicked()
    {
        iAPManager.BuyExtraLife();
    }
}
