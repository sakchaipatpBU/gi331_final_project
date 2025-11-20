using UnityEngine;

public class WorldEventSpriteManager : MonoBehaviour
{
    public GameObject rainObj;
    public GameObject powerOutageObj;
    public string eventPowerOutageName;
    public string eventRainingName;

    #region singleton
    private static WorldEventSpriteManager instance;
    public static WorldEventSpriteManager GetInstance()
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

    private void Start()
    {
        rainObj.SetActive(false);
        powerOutageObj.SetActive(false);
    }
    public void Init(string eventName)
    {
        if(eventName == eventRainingName)
        {
            rainObj.SetActive(true);

        }
        else if (eventName == eventPowerOutageName)
        {
            powerOutageObj.SetActive(true);
        }
    }
    public void ClearEvent(string eventName)
    {
        if (eventName == eventRainingName)
        {
            rainObj.SetActive(false);

        }
        else if (eventName == eventPowerOutageName)
        {
            powerOutageObj.SetActive(false);
        }
    }
}
