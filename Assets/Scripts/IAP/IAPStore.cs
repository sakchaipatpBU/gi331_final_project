using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;
using System;

public class IAPStore : MonoBehaviour
{
    [Header("Consumable")]
    public TextMeshProUGUI Cash;

    [Header("Non Consmabel")]
    public GameObject adsPurchasedWindow;
    //public GameObject adsBanner;

    [Header("Subscription")]
    public GameObject subActivateWindow;
    public GameObject premiumLogo;

    public Money money;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPurchaseFaild(Product product, PurchaseFailureDescription purchaseFailureDescription)
    {
        Debug.Log(product.definition.id);
        Debug.Log(purchaseFailureDescription.reason);
    }

    public void OnpurchaseChas10Complete(Product product)
    {
        Debug.Log(product.definition.id);
        money.AddMoney(100);
    }

    #region Non-Consumable
    void DisplayAds(bool active)
    {
        if (!active)
        {
            adsPurchasedWindow.SetActive(true);
            AdsSample.Instance.HideBannerAds();
        }
        else
        {
            adsPurchasedWindow.SetActive(false);
        }
    }
    void RemoveAds()
    {
        DisplayAds(false);
    }
    void ShowAds()
    {
        DisplayAds(true);
    }

    public void OnpurchaseRemoveAdsCompleta(Product product)
    {
        Debug.Log(product.definition.id);
        RemoveAds();
    }

    public void CheckNonConsumable(Product product)
    {
        if (product != null)
        {
            if (product.hasReceipt)
            {
                RemoveAds();
            }
            else
            {
                ShowAds();
            }
        }
    }
    #endregion
    #region Subscription
    void SetupCash(bool active)
    {
        money.isSub = true;
        if (active)
        {
            subActivateWindow.SetActive(true);
        }
        else
        {
            subActivateWindow.SetActive(false);
        }
        Debug.Log("X2");
    }
    void ActvateCashX2()
    {
        SetupCash(true);
    }
    public void OnpurchaseActivateChasX2(Product product)
    {
        Debug.Log(product.definition.id);
        ActvateCashX2();
    }
    #endregion
}
