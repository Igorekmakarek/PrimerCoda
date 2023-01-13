using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAds : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private bool _testMode = true;
    [SerializeField] private Button _adsButton;

    private string _gameId = "4566050"; 

    private string _rewardedVideo = "Rewarded_iOS";

    void Start()
    {
        _adsButton = GetComponent<Button>();
        _adsButton.interactable = Advertisement.IsReady(_rewardedVideo);

        if (_adsButton)
            _adsButton.onClick.AddListener(ShowRewardedVideo);

        Advertisement.AddListener(this);
        Advertisement.Initialize(_gameId, true);
    }

    public void ShowRewardedVideo()
    {
        Advertisement.Show(_rewardedVideo);
    }

    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == _rewardedVideo)
        {
            _adsButton.interactable = true; //действия если реклама доступна
        }
    }

    public void OnUnityAdsDidError(string message)
    {
        //ошибка рекламы
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        //только началась реклама
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult) //обработка рекламы (определяем вознаграждение)
    {
        if (showResult == ShowResult.Finished)
        {
            if (placementId == "Interstitial_iOS")
                ShopScript.instance.GetCoins(50);
                


            //действия если пользователь досмотрел рекламу до конца 
        }
        else if (showResult == ShowResult.Skipped)
        {
            //если пропустил рекламу
        }
        else if (showResult == ShowResult.Failed)
        {
            //если ошибка
        }
    }
}

