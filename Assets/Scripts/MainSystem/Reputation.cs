using System.Collections;
using UnityEngine;

public class Reputation : MonoBehaviour
{
    public int currentReputation;
    public int minReputation;
    public int maxReputation;
    public float baseVisitPercent;

    
    // concept Reputation ยิ่งมาก คนยิ่งเยอะ => ส่งผลต่อโอกาสเช่าเยอะขึ้น


    #region Singleton
    private static Reputation instance;
    public static Reputation Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    #endregion

    private void OnEnable()
    {
        TimeManager.OnNewDay += NewVisitor;
    }
    private void OnDisable()
    {
        TimeManager.OnNewDay -= NewVisitor;
    }

    private void Start()
    {
        SaveManager.LoadReputation();
    }
    public void AddReputation(int amount)
    {
        currentReputation += amount;
        currentReputation = Mathf.Clamp(currentReputation, minReputation, maxReputation);
    }
    public void ReduceReputation(int amount)
    {
        currentReputation -= amount;
        currentReputation = Mathf.Clamp(currentReputation, minReputation, maxReputation);

    }

    private void NewVisitor()
    {
        float percent = baseVisitPercent + (Reputation.Instance.currentReputation / 100);
        float rand = Random.Range(0f, 100f);
        if(percent >= rand)
        {
            RenterManager.Instance.CreateNewRenter();
        }
    }
    
}
