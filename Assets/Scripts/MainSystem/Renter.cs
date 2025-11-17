using UnityEngine;

public class Renter : MonoBehaviour
{
    public int currentRep;
    public int minRep;
    public int maxRep;
    public int rentCost;

    public Room rentRoom;
    public Transform startPath;
    public Transform endPath;
    public Vector3 roomOffset;

    public Sprite[] renterSprite; // สุ่ม sprite

    public void Initialize(Room room)
    {

    }

    public void AddRenterRep(int amount)
    {
        currentRep += amount;
        currentRep = Mathf.Clamp(currentRep, minRep, maxRep);
    }
    public void ReduceRenterRep(int amount)
    {
        currentRep -= amount;
        currentRep = Mathf.Clamp(currentRep, minRep, maxRep);
    }

    public void CalculateRentCost()
    {

    }
}
