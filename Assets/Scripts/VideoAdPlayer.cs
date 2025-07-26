using UnityEngine;
using TapsellPlusSDK;
public class VideoAdPlayer : MonoBehaviour
{
    public string _responseId;
    public GameManager gm;
    private void OnEnable()
    {
        TapsellPlus.Initialize
        (
            "nhotqkddnfrtkkrimidloobekliaooearfpfjsccmldbdtelhfsfmfslnfmbjccfanqnjr",
            adNetworkName => Debug.Log(adNetworkName + " Initialized Successfully."),
            error => Debug.Log(error.ToString())
        );
        TapsellPlus.SetGdprConsent(true);
        RequestAds();
    }
    public void RequestAds()
    {
        TapsellPlus.RequestRewardedVideoAd
        (
        "68814ac569af8c7f63545215",

        tapsellPlusAdModel =>
        {
            Debug.Log("on response " + tapsellPlusAdModel.responseId);
            _responseId = tapsellPlusAdModel.responseId;
        },

            error => Debug.Log("Error " + error.message)

        );
    }
    public void Show()
    {
        TapsellPlus.ShowRewardedVideoAd
        (
            _responseId,

            tapsellPlusAdModel => Debug.Log("onOpenAd " + tapsellPlusAdModel.zoneId),
            tapsellPlusAdModel => gm.Relive(),
            tapsellPlusAdModel => Debug.Log("onCloseAd " + tapsellPlusAdModel.zoneId),

            error => Debug.Log("onError " + error.errorMessage)
        );
    }
}