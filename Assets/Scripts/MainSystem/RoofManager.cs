using UnityEngine;

public class RoofManager : MonoBehaviour
{
    public GameObject roofPrefab;
    public GameObject roof;
    public Transform roofParent;
    public Vector3 pos;

    #region Singleton
    private static RoofManager instance;
    public static RoofManager Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    private void Start()
    {
        roof = Instantiate(roofPrefab, roofParent);
        SetRoof();
    }

    public void SetRoof()
    {
        pos.y = ElevetorManager.Instance.allFloor + 0.5f;
        roof.transform.position = pos;
    }
}
