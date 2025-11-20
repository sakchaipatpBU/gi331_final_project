using UnityEngine;
using Unity.Services.LevelPlay;

public class AdsSample : MonoBehaviour
{
    private LevelPlayBannerAd bannerAd;
    private LevelPlayInterstitialAd interstitialAd;
    private LevelPlayRewardedAd rewardedVideoAd;

    public bool isAdsEnabled = false;

    #region Singleton
    private static AdsSample instance;
    public static AdsSample Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    void Start()
    {
        LevelPlay.ValidateIntegration();
        LevelPlay.OnInitSuccess += SdkInitCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitFailedEvent;
        LevelPlay.Init(AdConfig.AppKey);
    }
    
    private void SdkInitCompletedEvent(LevelPlayConfiguration configuration)
    {
        Debug.Log($"Ads Enable");
        EnableAds();
        isAdsEnabled = true;
    }
    private void SdkInitFailedEvent(LevelPlayInitError error)
    {
        Debug.Log($"Level Play Error : {error}");
    }

    void EnableAds()
    {
        var configBuilder = new LevelPlayBannerAd.Config.Builder();
        configBuilder.SetSize(LevelPlayAdSize.BANNER)
            .SetPosition(LevelPlayBannerPosition.TopCenter);
        var bannerConfig = configBuilder.Build();

        bannerAd = new LevelPlayBannerAd(AdConfig.BannerAdUnitId, bannerConfig);

        interstitialAd = new LevelPlayInterstitialAd(AdConfig.InterstitialAdUnitId);
        interstitialAd.OnAdClosed += InterstitialOnAdCloseEvent;

        rewardedVideoAd = new LevelPlayRewardedAd(AdConfig.RewardedVideoAdUnitId);
        rewardedVideoAd.OnAdRewarded += RewardedVideoOnAdRewardedEvent;
        rewardedVideoAd.OnAdClosed += RewardedVideoOnAdClosedEvent;

        LoadBannerAds();
    }
    
    private void InterstitialOnAdCloseEvent(LevelPlayAdInfo info)
    {
        //GameManager.GetInstance().OnBackClicked();
    }

    private void RewardedVideoOnAdRewardedEvent(LevelPlayAdInfo info, LevelPlayReward reward)
    {
        Money.Instance.AddMoney(300);
    }
    private void RewardedVideoOnAdClosedEvent(LevelPlayAdInfo info)
    {
        //Debug.Log("Rewarded Video Ad is Closed");
    }
    public void LoadBannerAds()
    {
        bannerAd.LoadAd();
    }
    public void HideBannerAds()
    {
        bannerAd.HideAd();
    }
    public void ShowBannerAds()
    {
        bannerAd.ShowAd();
    }
    public void LoadInterstitialAd()
    {
        interstitialAd.LoadAd();
    }
    public void ShowInterstitialAd()
    {
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd();
        }
        else
        {
            Debug.Log("{LevelPlaySample} LevelPlay Interstitial Ad is not ready");
        }
    }
    public void LoadRewardedVideoAd()
    {
        rewardedVideoAd.LoadAd();
    }
    public void ShowRewardedVideoAd()
    {
        if (rewardedVideoAd.IsAdReady())
        {
            rewardedVideoAd.ShowAd();
        }
        else
        {
            Debug.Log("{LevelPlaySample} LevelPlay Rewarded Video Ad is not ready");
        }
    }
    private void OnDisable()
    {
        bannerAd.DestroyAd();
        interstitialAd.DestroyAd();
        rewardedVideoAd.DestroyAd();
    }
}
