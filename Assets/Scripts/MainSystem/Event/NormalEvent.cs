using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalEvent : Event
{
    public bool isInteract;
    public Room room;

    public int satisfaction = 20;

    public void Init(Room _room)
    {
        Debug.Log("NormalEvent => Init");

        room = _room;
        renter = room.renter;
    }
    private void Start()
    {
        StartCoroutine(TimerCoroutine());
        CreateEvent();

    }
    IEnumerator TimerCoroutine()
    {
        while (duration < maxDuration)
        {
            if (isInteract) // กด แก้ไข event
            {

                yield break;
            }
            yield return null;
        }
        // ไม่กด แก้ไข event
        ReduceReputation(); // ! ลดคร้งเดียว

    }

    // สร้าง obj event
    public void CreateEvent()
    {
        // สร้าง event ไว้ที่ห้อง
        GameObject eventObj = Instantiate(eventPrefab, room.gameObject.transform.position, Quaternion.identity);

        // ต้องมี collider
        if (eventObj.GetComponent<Collider2D>() == null)
        {
            eventObj.AddComponent<BoxCollider2D>();
        }
        // เพิ่ม EventTrigger Component
        EventTrigger trigger = eventObj.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = eventObj.AddComponent<EventTrigger>();
        }

        // สร้าง EventTrigger.Entry สำหรับ PointerClick
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;

        // ใส่ Action เวลากดคลิก
        clickEntry.callback.AddListener((eventData) =>
        {
            this.OnNormalEventClicked();
            Destroy(eventObj.gameObject);
        });

        // เพิ่ม entry เข้า EventTrigger
        trigger.triggers.Add(clickEntry);
    }

    // เรียกจาก eventPrefab
    public void OnNormalEventClicked()
    {
        AddReputation();
        isInteract = true;
        EventManager.GetInstance().RemoveNormalEvent(this);
        Destroy(this.gameObject);
    }

    #region Reputation
    public void AddReputation()
    {
        renter.AddRenterSatisfaction(satisfaction);
    }
    public void ReduceReputation()
    {
        renter.ReduceRenterSatisfaction(satisfaction);
    }
    #endregion

}
