using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject RoomPrefab;
    public Transform emptyRoomParent;
    public Vector3 startPos; // ตำแหน่งห้องแรก

    public int column; // ต้องเป็นเลขจำนวนเต็มคู่ => 2, 4, 6, 8 ...

    public int floorCount;

    public int moneyDaily;
    public int reputationDaily;
    public List<Room> allRoom = new List<Room>();

    #region Singleton
    private static RoomManager instance;
    public static RoomManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion
    private void OnEnable()
    {
        TimeManager.OnNewDay += AddMoneyDaily;
        TimeManager.OnNewDay += AddReputationDaily;
    }
    private void OnDisable()
    {
        TimeManager.OnNewDay -= AddMoneyDaily;
        TimeManager.OnNewDay -= AddReputationDaily;
    }
    private void Start()
    {
        floorCount = ElevetorManager.Instance.allFloor;
        GenerateBuilding();


    }
    
    void GenerateBuilding()
    {
        for (int i = 0; i < column; i++)
        {
            for (int j = 0; j < floorCount; j++)
            {
                float spacing = 2f;
                float middleIndex = (column) / 2f;
                float posX = 0;
                if (i < middleIndex)
                {
                    posX = (i - middleIndex) * spacing + 1;
                }
                else
                {
                    posX = (i - middleIndex + 1) * spacing;

                }

                float posY = startPos.y + j;

                GameObject room = Instantiate(RoomPrefab
                , new Vector3(posX, posY, startPos.z)
                , Quaternion.identity
                , emptyRoomParent);

                room.name = $"Room [{i},{j}]";

                Room newRoom = room.GetComponent<Room>();
                allRoom.Add(newRoom);
            }

        }
    }
    void CreateNewRoomFloor() // Feature - สร้างชั้นใหม่
    {

    }
    public void AddMoneyDaily()
    {
        for(int i = allRoom.Count - 1; i >= 0; i--)
        {
            if (allRoom[i].roomType != RoomType.Empty && 
                allRoom[i].roomType != RoomType.Bedroom)
            {
                Money.Instance.AddMoney(CalMoneyPerDay());
            }
        }
    }

    private int CalMoneyPerDay()
    {
        int total = moneyDaily + (Reputation.Instance.currentReputation / 100);
        Debug.Log("RoomManager - AddMoneyDaily " + total);
        return total;
    }
    public void AddReputationDaily()
    {
        for (int i = allRoom.Count - 1; i >= 0; i--)
        {
            if (allRoom[i].roomType != RoomType.Empty &&
                allRoom[i].roomType != RoomType.Bedroom)
            {
                Reputation.Instance.AddReputation(reputationDaily);
            }
        }
    }

}
