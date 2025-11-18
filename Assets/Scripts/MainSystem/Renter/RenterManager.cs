using System.Collections.Generic;
using UnityEngine;

public class RenterManager : MonoBehaviour
{

    public GameObject renterPrefab;
    public Transform renterParent;
    public Vector3 renterSpawnPos;

    public List<Renter> allRenters = new List<Renter>();
    public int onStartRenter;

    #region Singleton
    private static RenterManager instance;
    public static RenterManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        SaveManager.LoadRenterCount();
        GenerateAllRenter();
    }
    #endregion

    private void Start()
    {
        
    }
    public void CreateNewRenter()
    {
        GameObject rObj = Instantiate(renterPrefab, renterSpawnPos, Quaternion.identity, renterParent);
        Renter renter = rObj.GetComponent<Renter>();
        allRenters.Add(renter);
    }
    public void GenerateAllRenter()
    {
        if(allRenters.Count != 0)
        {
            allRenters.Clear();
        }

        for(int i = 0; i < onStartRenter; i++)
        {
            GameObject rObj = Instantiate(renterPrefab, renterSpawnPos, Quaternion.identity, renterParent);
            Renter renter = rObj.GetComponent<Renter>();
            allRenters.Add(renter);
        }
    }

}
