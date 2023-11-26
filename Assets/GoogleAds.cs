using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class GoogleAds : MonoBehaviour
{
    public static GoogleAds instance;
    private InterstitialAd interstitialAd;

    // Start is called before the first frame update
    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        Debug.Log("GoogleAds: Google Ads is starting to Initialize");
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            Debug.Log("initStatus: " + initStatus.ToString());
            Debug.Log("GoogleAds: Google Ads has Initialized");
            LoadInterstitialAd();
        });

    }


        #if UNITY_ANDROID
            private string _adUnitId = "ca-app-pub-2169102625145244/4252954949";
        #elif UNITY_IPHONE
          private string _adUnitId = "ca-app-pub-3940256099942544/4411468910";
        #else
          private string _adUnitId = "unused";
        #endif


    /// <summary>
    /// Loads the interstitial ad.
    /// </summary>
    public void LoadInterstitialAd()
    {
        Debug.Log("GoogleAds: Google Ads is Loading LoadInterstitialAd");

        // Clean up the old ad before loading a new one.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("GoogleAds: Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(_adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("GoogleAds: Interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("GoogleAds: Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
                RegisterEventHandlers(interstitialAd);
            });
    }

    public void ShowAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("GoogleAds: Showing interstitial ad.");
            interstitialAd.Show();
        }
        else
        {
            Debug.LogError("GoogleAds: Interstitial ad is not ready yet.");
        }
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("GoogleAds: Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("GoogleAds: Interstitial ad full screen content closed.");
            LoadInterstitialAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("GoogleAds: Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            LoadInterstitialAd();
        };
    }
}
