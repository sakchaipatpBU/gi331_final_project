using System;
using UnityEngine;

public class Money : MonoBehaviour
{
    public TimeManager timeManager;

    public int currentMoney;
    public static Action OnChangeMoney;

    #region Singleton
    private static Money instance;
    public static Money Instance { get { return instance; } }
    public bool isSub = false;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        if (timeManager == null)
        {
            timeManager = TimeManager.Instance;
        }
    }

    public void AddMoney(int amount)
    {
        if (isSub == true)
        {
            amount *= 2;
        }
        currentMoney += amount;
        if (OnChangeMoney != null)
        {
            OnChangeMoney();
        }

    }

    public bool TrySpend(int amount)
    {
        if (currentMoney < amount) return false;
        currentMoney -= amount;
        if (OnChangeMoney != null)
        {
            OnChangeMoney();
        }
        return true;
    }
}
