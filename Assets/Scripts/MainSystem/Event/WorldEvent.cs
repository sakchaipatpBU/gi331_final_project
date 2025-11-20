using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldEvent : Event
{
    public List<Room> rooms;

    public List<GameObject> events;

    public int repairCost = 200;
    public bool isEventEnd = false;

    public List<Renter> renters;

    public void Init(List<Room> _rooms)
    {
        Debug.Log("WorldEvent => Init");

        rooms = _rooms;

        CreateEvent();
        WorldEventSpriteManager.GetInstance().Init(eventName);
        if (renters.Count != 0)
        {
            renters.Clear();
        }
        for (int i = 0; i < rooms.Count; i++) //ปิดห้อง >> ห้องเช่าไม่นับวันเพิ่ม 
        {
            Renter r = rooms[i].renter;
            if (r != null)
            {
                renters.Add(r);
                renters[i].enabled = false;
            }
        }
    }

    public override void DayPassed()
    {
        if (isEventEnd) return;
        base.DayPassed();
        if (duration >= maxDuration) // กดซ่อมได้
        {
            isEventEnd = true;
            AddEventTrigger();
            WorldEventSpriteManager.GetInstance().ClearEvent(eventName);
        }
    }
    public void CreateEvent()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            // สร้าง event ไว้ที่ห้อง  !! re-check ref ห้อง !!
            GameObject eventObj = Instantiate(eventPrefab, rooms[i].gameObject.transform.position, Quaternion.identity);

            events.Add(eventObj);
        }
    }

    public void AddEventTrigger()
    {
        foreach (GameObject eventObj in events)
        {
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
                this.OnWorldEventClicked(eventObj);
                Destroy(eventObj.gameObject);
            });

            // เพิ่ม entry เข้า EventTrigger
            trigger.triggers.Add(clickEntry);
        }
    }
    public void OnWorldEventClicked(GameObject eventObject)
    {
        bool repaired = Money.Instance.TrySpend(repairCost);
        if (repaired)
        {
            foreach (GameObject eventObj in events) // หาห้องที่ซ่อมแล้ว เพื่อเปิดใช้งาน
            {
                if (eventObject == eventObj)
                {
                    RoomRepaired(events.IndexOf(eventObj));
                    break;
                }
            }
            Destroy(eventObject);
            events.Remove(eventObject);
        }
        if(events.Count == 0)
        {
            EventManager.GetInstance().RemoveWorldEvent(this);
            Destroy(this.gameObject);
        }
    }
    // เปิดใช้งานห้อง
    public void RoomRepaired(int idx)
    {
        renters[idx].enabled = true;
    }
}
