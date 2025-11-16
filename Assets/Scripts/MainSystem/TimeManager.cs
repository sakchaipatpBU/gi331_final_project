using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int day = 1;
    public int month = 1;
    public int year = 1;
    public float timeSpeed = 1f;
    private float timer;
    // Event ต่าง ๆ
    public static Action OnNewDay;
    public static Action OnNewMonth;
    public static Action OnNewYear;

    #region Singleton
    private static TimeManager instance;
    public static TimeManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    void Update()
    {
        timer += Time.deltaTime * timeSpeed;

        if (timer >= 24) // 24 วินาทีในเกม = 1 วัน
        {
            timer = 0;
            AddDay();
        }
    }
    void AddDay()
    {
        day++;
        if (OnNewDay != null)
        {
            OnNewDay();
        }

        if (day > 30)
        {
            day = 1;
            AddMonth();
        }
    }
    void AddMonth()
    {
        month++;
        if (OnNewMonth != null)
        {
            OnNewDay();
        }

        if (month > 12)
        {
            month = 1;
            AddYear();
        }
    }
    void AddYear()
    {
        year++;
        if (OnNewYear != null)
        {
            OnNewDay();
        }
    }

    #region Fast Forward

    public void OnNormalTimeScale()
    {
        timeSpeed = 1f;
    }
    public void On3SecPerDayTimeScale()
    {
        timeSpeed = 8f;
    }
    public void On6SecPerDayTimeScale()
    {
        timeSpeed = 4f;
    }
    public void On12SecPerDayTimeScale()
    {
        timeSpeed = 2f;
    }

    #endregion
}
