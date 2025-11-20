using UnityEngine;

public class Renter : MonoBehaviour
{
    public int currentSatisfaction;
    public int minSatisfaction;
    public int maxSatisfaction;
    public string namePrefix = "Renter";
    public Room rentRoom;
    public float percentToRent;
    public int baseRent;
    public Transform startPath;
    public Transform endPath;
    public Vector3 roomOffset;


    public Sprite[] renterSprite; // สุ่ม sprite

    public int startDate;
    public int startMonth;
    public int endDate;
    public int endMonth;
    private int[] daysInMonth =
    {
        31, 28, 31, 30, 31, 30,
        31, 31, 30, 31, 30, 31
    };


    private void OnEnable()
    {
        TimeManager.OnNewDay += CheckIn;
        TimeManager.OnNewDay += CheckOut;
        //TimeManager.OnNewDay += Leave;
    }
    private void OnDisable()
    {
        TimeManager.OnNewDay -= CheckIn;
        TimeManager.OnNewDay -= CheckOut;
        //TimeManager.OnNewDay -= Leave;
    }
    public void Initialize(Room room)
    {
        rentRoom = room;
        gameObject.name = namePrefix + room.name;

        LoadRenter();
        room.SetRenter(this, true);
        baseRent = room.BaseRent;

        transform.position = room.transform.position + roomOffset;
    }
    private void Update()
    {
        if (rentRoom != null) return;

        // behav
    }
    public void SaveRenter()
    {
        SaveManager.SaveRenter(this);
    }
    public void LoadRenter()
    {
        SaveManager.LoadRenter(this);
    }
    public void CheckIn()
    {
        if (rentRoom != null) return;

        float percent = percentToRent + (Reputation.Instance.currentReputation / 100); // โอกาสเช่าห้อง
        float rand = Random.Range(0f, 100f);
        if (percent >= rand)
        {
            int allRoomCount = RoomManager.Instance.allRoom.Count;
            for (int i = allRoomCount - 1; i >= 0; i--)
            {
                if (RoomManager.Instance.allRoom[i].renter == null
                    && RoomManager.Instance.allRoom[i].roomType == RoomType.Bedroom)
                {
                    Initialize(RoomManager.Instance.allRoom[i]);

                    // สุ่มวันออก
                    startDate = TimeManager.Instance.day;
                    startMonth = TimeManager.Instance.month;
                    endDate = Random.Range(1, 28);
                    endMonth = Random.Range(startMonth, startMonth + 2);
                    if (endMonth > 12) endMonth -= 12;

                    return;
                }
            }
        }
        else
        {
            RenterManager.Instance.RenterLeave(this);
            Destroy(gameObject, 1f);
        }
    }
    public void CheckOut()
    {
        if( TimeManager.Instance.day != endDate ||
            TimeManager.Instance.month != endMonth) return;

        //จ่าย - ออก
        Money.Instance.AddMoney(CalculateRentCost());
        Debug.Log($"Incom from {gameObject.name} leave");
        rentRoom.SetRenter(this, false);
        RenterManager.Instance.RenterLeave(this);

        Destroy(gameObject, 1f);
    }

    /*public void Leave()
    {
        if (rentRoom != null) return;


    }*/
    public void AddRenterSatisfaction(int amount)
    {
        currentSatisfaction += amount;
        currentSatisfaction = Mathf.Clamp(currentSatisfaction, minSatisfaction, maxSatisfaction);
    }
    public void ReduceRenterSatisfaction(int amount)
    {
        currentSatisfaction -= amount;
        currentSatisfaction = Mathf.Clamp(currentSatisfaction, minSatisfaction, maxSatisfaction);
    }

    public int CalculateRentCost()
    {
        int cost = baseRent + (currentSatisfaction / 10);
        cost = Mathf.Clamp(cost, (int)(baseRent * 0.5f), (int)(baseRent * 5f));
        return CalculateTotalDays() * cost;
    }
    public int CalculateTotalDays()
    {
        int total = 0;

        // ข้ามปี
        bool isCrossYear = endMonth < startMonth;

        if (!isCrossYear)
        {
            if (startMonth == endMonth)
            {
                return endDate - startDate;
            }

            // เดือนแรก
            total += daysInMonth[startMonth - 1] - startDate;

            // เดือนกลาง
            for (int m = startMonth + 1; m < endMonth; m++)
            {
                total += daysInMonth[m - 1];
            }

            // เดือนสุดท้าย
            total += endDate;

            return total;
        }
        else
        {
            // เดือนแรก (ปีเก่า)
            total += daysInMonth[startMonth - 1] - startDate;

            // เดือนหลัง startMonth ในปีเก่า
            for (int m = startMonth + 1; m <= 12; m++)
            {
                total += daysInMonth[m - 1];
            }

            // เดือนต้นปีใหม่จนถึงเดือนก่อน endMonth
            for (int m = 1; m < endMonth; m++)
            {
                total += daysInMonth[m - 1];
            }

            // เดือนสุดท้าย
            total += endDate;

            return total;
        }
    }

}
