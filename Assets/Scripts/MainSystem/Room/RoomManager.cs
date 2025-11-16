using UnityEngine;
using UnityEngine.UIElements;

public class RoomManager : MonoBehaviour
{
    public GameObject RoomPrefab;
    public Transform emptyRoomParent;
    public Vector3 startPos; // ตำแหน่งห้องแรก

    public int column; // ต้องเป็นเลขจำนวนเต็มคู่ => 2, 4, 6, 8 ...

    public int floorCount;


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
            }

        }
    }

}
