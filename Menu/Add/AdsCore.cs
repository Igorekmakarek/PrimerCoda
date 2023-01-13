
using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class AdsCore : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = true;

    private string _gameID = "4566050";

    private string _video = "Interstitial_iOS";
    private string _rewardedVideo = "Rewarded_iOS";
    private string _banner = "Banner_iOS";

    private void Start()
    {
        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameID, _testMode);

        #region Banner
        StartCoroutine(ShowBannerWhenInitialized());
        Advertisement.Banner.SetPosition(BannerPosition.BOTTOM_CENTER);

        #endregion
    }

    public static void ShowAdsVideo(string placementID)
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show(placementID);
        }
        else
        {
            Debug.Log("Ad is not ready!");
        }
    }

    IEnumerator ShowBannerWhenInitialized()
    {
        while (!Advertisement.isInitialized)
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(_banner);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _rewardedVideo)
        {
            //действия, если реклама доступна
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //ошибка рекламы
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //только запустили рекламу
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)      //обработка рекламы (определяем вознаграждение)
    {
        if (showResult == ShowResult.Finished)
        {
            //действия если пользователь досмотрел рекламу до конца
        }
        else if (showResult == ShowResult.Skipped)
        {
            //действия если пользователь пропустил рекламу
        }
        else if (showResult == ShowResult.Failed)
        {
            //действия при ошибке
        }
    }
}
