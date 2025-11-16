using UnityEngine;

public class ElevetorManager : MonoBehaviour
{
    public int baseFloor = 5;
    public int floorCount; // จำนวนชั้นที่ add เพิ่ม (ไม่รวม base)
    public int allFloor;
    public GameObject elevetorPrefab;
    public Transform elevetorParent;  
    public Vector3 startPos; // ตำแหน่งห้องแรก


    #region Singleton
    private static ElevetorManager instance;
    public static ElevetorManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        SaveManager.LoadFloorCount();
        allFloor = baseFloor + floorCount;
    }
    #endregion

    private void Start()
    {

        for(int i = 0; i < allFloor; i++)
        {
            float posY = i + startPos.y;
            Instantiate(elevetorPrefab
                , new Vector3(startPos.x, posY, startPos.z)
                , Quaternion.identity
                , elevetorParent);
        }
    }
}
