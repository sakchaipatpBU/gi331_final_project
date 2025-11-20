using UnityEngine;

public class Event : MonoBehaviour
{
    public Renter renter;

    public string eventName;

    public int duration = 0; // นับเป็นวัน
    public int maxDuration; // จำนวนวันที่ต้องการให้เกิด event

    public GameObject eventPrefab;

    public virtual void DayPassed()
    {
        duration++;
    }

    private void OnEnable()
    {
        TimeManager.OnNewDay += DayPassed;
    }

    private void OnDisable()
    {
        TimeManager.OnNewDay -= DayPassed;
    }
}
