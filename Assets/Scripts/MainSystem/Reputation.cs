using JetBrains.Annotations;
using UnityEngine;

public class Reputation : MonoBehaviour
{
    public int currentReputation;
    public int minReputation;
    public int maxReputation;

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

    public void AddReputation(int amount)
    {
        currentReputation += amount;
    }
    public void ReduceReputation(int amount)
    {
        currentReputation += amount;
    }
}
