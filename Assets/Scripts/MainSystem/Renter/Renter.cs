using UnityEngine;

public class Renter : MonoBehaviour
{
    public int currentSatisfaction;
    public int minSatisfaction;
    public int maxSatisfaction;
    public int rentCost;

    public Room rentRoom;
    public Transform startPath;
    public Transform endPath;
    public Vector3 roomOffset;

    public Sprite[] renterSprite; // สุ่ม sprite

    public int startDate;
    public int startMonth;
    public int endDate;
    public int endMonth;

    public void Initialize(Room room)
    {

    }

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

    public void CalculateRentCost()
    {

    }
}
