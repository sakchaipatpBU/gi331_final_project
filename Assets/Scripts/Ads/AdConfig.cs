using UnityEngine;

public static class AdConfig
{
    public static string AppKey => GetAppKey();
    public static string BannerAdUnitId => GetBannerAdUnitId();
    public static string InterstitialAdUnitId => GetInnterstitialAdUnitId();
    public static string RewardedVideoAdUnitId => GetRewardedVideoAdUnitId();

    static string GetAppKey()
    {
#if UNITY_ANDROID
        return "5989407";
#elif UNITY_IPHONE
        return "5989406";
#else
        return "unexpected_platform";
#endif
    }

    static string GetBannerAdUnitId()
    {
#if UNITY_ANDROID
        return "rh6p3n087tvp6ejy";
#elif UNITY_IPHONE
        return "rh6p3n087tvp6ejy";
#else
        return "unexpected_platform";
#endif
    }

    static string GetInnterstitialAdUnitId()
    {
#if UNITY_ANDROID
        return "7hi7v78y39mz8lx7";
#elif UNITY_IPHONE
        return "7hi7v78y39mz8lx7";
#else
        return "unexpected_platform";
#endif
    }

    static string GetRewardedVideoAdUnitId()
    {
#if UNITY_ANDROID
        return "gvmt696854b402tu";
#elif UNITY_IPHONE
        return "gvmt696854b402tu";
#else
        return "unexpected_platform";
#endif
    }
}
