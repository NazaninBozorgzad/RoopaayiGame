using UnityEngine;
using TapsellPlusSDK;
using TMPro;
public class VideoAdPlayer : MonoBehaviour
{
    public string _responseId;
    public TextMeshProUGUI response;
    private int requestCount = 0;
    public GameManager gm;
    private void OnEnable()
    {
        requestCount = 0;
        TapsellPlus.Initialize
        (
            "nhotqkddnfrtkkrimidloobekliaooearfpfjsccmldbdtelhfsfmfslnfmbjccfanqnjr",
            adNetworkName => Debug.Log(adNetworkName + " Initialized Successfully."),
            error => 
            {
                response.text = "Ad initialization failed.";
                response.color = Color.red;
            }
        );
        TapsellPlus.SetGdprConsent(true);
        RequestAds();
    }
    public void RequestAds()
    {
        if (requestCount <= 10000)
        {
            TapsellPlus.RequestRewardedVideoAd
            (
                "68814ac569af8c7f63545215",

                tapsellPlusAdModel =>
                {
                    _responseId = tapsellPlusAdModel.responseId;
                },

                error =>
                {
                    response.text = "Failed to request ad. Retrying...";
                    response.color = Color.red;
                }

            );

            requestCount++;
        }
    }
    public void Show()
    {
        TapsellPlus.ShowRewardedVideoAd
        (
            _responseId,

            tapsellPlusAdModel => Debug.Log("onOpenAd " + tapsellPlusAdModel.zoneId),
            tapsellPlusAdModel => gm.Relive(),
            tapsellPlusAdModel => Debug.Log("onCloseAd " + tapsellPlusAdModel.zoneId),

            error => RequestAds()
        );
    }
}