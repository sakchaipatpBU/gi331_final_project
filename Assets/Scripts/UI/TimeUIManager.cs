using System;
using TMPro;
using UnityEngine;

public class TimeUIManager : MonoBehaviour
{
    public TimeManager timeManager;

    public TMP_Text dayText;
    public TMP_Text monthText;
    public TMP_Text yearText;

    public string dayPrefix = "D. ";
    public string monthPrefix = "M. ";
    public string yearPrefix = "Year : ";

    void Start()
    {
        if (timeManager == null)
        {
            timeManager = TimeManager.Instance;
        }


        // update ui ตอนเริ่มเกม
        DayPass();
        MonthPass();
        YearPass();
    }

    private void OnEnable()
    {
        TimeManager.OnNewDay += DayPass;
        TimeManager.OnNewMonth += MonthPass;
        TimeManager.OnNewYear += YearPass;
    }
    private void OnDisable()
    {
        TimeManager.OnNewDay -= DayPass;
        TimeManager.OnNewMonth -= MonthPass;
        TimeManager.OnNewYear -= YearPass;
    }
    private void DayPass()
    {
        dayText.text = dayPrefix + timeManager.day.ToString();
    }
    private void MonthPass()
    {
        monthText.text = monthPrefix + timeManager.month.ToString();
    }
    private void YearPass()
    {
        yearText.text = yearPrefix + timeManager.year.ToString();
    }
}
