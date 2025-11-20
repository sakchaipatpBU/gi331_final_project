using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public List<NormalEvent> activeNormalEvents = new List<NormalEvent>();
    public List<WorldEvent> activeWorldEvents = new List<WorldEvent>();
    public GameObject[] normalEventPrefabs;
    public GameObject[] worldEventPrefabs;

    public Transform eventParent;

    public float percentTriggerEvent;

    public int roomCount;
    #region singleton
    private static EventManager instance;
    public static EventManager GetInstance()
    {
        return instance;
    }
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }
    #endregion
    //
    /*public void SetEventManager(TimeController _timeController, TenantManager _tenantManager)
    {
        timeController = _timeController;
        tenantManager = _tenantManager;
    }*/

    void OnEnable()
    {
        TimeManager.OnNewDay += CheckWorldEvents;
    }

    void OnDisable()
    {
        TimeManager.OnNewDay -= CheckWorldEvents;
    }

    //
    void CheckWorldEvents()
    {
        float rand = Random.Range(0f, 100f);
        Debug.Log("rand => " + rand);

        if (percentTriggerEvent > rand)
        {
            if (Random.value < 0.5f)
            {
                TriggerWorldEvent();
                Debug.Log("TriggerWorldEvent Event Today");

            }
            else
            {
                TriggerNormalEvent();
                Debug.Log("TriggerNormalEvent Event Today");

            }
        }
        else
        {
            Debug.Log("Not Event Today");
        }
    }

    void TriggerWorldEvent()
    {
        int roomCountToSetEvent = 3; // จำนวนห้องที่โดน world event
        List<Room> rooms = new List<Room>();
        for (int i = 0; i < roomCountToSetEvent; i++)
        {
            Room room = GetRandomRoomInScene();
            if (room == null)
            {
                return; // no room
            }

            bool hasRoom = false;
            foreach (Room r in rooms)
            {
                if (room == r)
                {
                    hasRoom = true;
                    break;
                }
            }
            if (!hasRoom)
            {
                rooms.Add(room);
            } // มีโอกาสได้แค่ห้องเดียว
        }

        if (roomCount <= 1) return;

        // สุ่ม world event 
        int randomEvent = Random.Range(0, worldEventPrefabs.Length);
        GameObject newEvent = Instantiate(worldEventPrefabs[randomEvent]
            , eventParent);

        WorldEvent worldEvent = newEvent.GetComponent<WorldEvent>();
        worldEvent.Init(rooms);
    }
    void TriggerNormalEvent()
    {
        Room room = GetRandomRoomInScene();
        if (room == null)
        {
            return; // no room
        }

        if (roomCount <= 1) return;

        // สุ่ม normal event 
        int randomEvent = Random.Range(0, normalEventPrefabs.Length);
        GameObject newEvent = Instantiate(normalEventPrefabs[randomEvent]
           , eventParent);

        NormalEvent normalEvent = newEvent.GetComponent<NormalEvent>();
        normalEvent.Init(room);
        activeNormalEvents.Add(normalEvent);
    }

    // get room
    public Room GetRandomRoomInScene()
    {
        // หาห้องทั้งหมด
        List<Room> allRoom = RoomManager.Instance.allRoom;
        List<Room> rooms = new List<Room>(); // ใช้เพื่อ random ห้อง

        for(int i = allRoom.Count - 1; i >= 0; i--)
        {
            if (allRoom[i].roomType == RoomType.Bedroom && allRoom[i].renter != null)
            {
                rooms.Add(allRoom[i]);
            }
        }
        SetRoomCount(rooms.Count);
        if (rooms.Count == 0) return null;
        int randomRoom = Random.Range(0, rooms.Count);
        return rooms[randomRoom];
    }
    private void SetRoomCount(int r)
    {
        roomCount = r;
    }

    public void RemoveNormalEvent(NormalEvent _event)
    {
        activeNormalEvents.Remove(_event);
    }
    public void RemoveWorldEvent(WorldEvent _event)
    {
        activeWorldEvents.Remove(_event);
    }
}
