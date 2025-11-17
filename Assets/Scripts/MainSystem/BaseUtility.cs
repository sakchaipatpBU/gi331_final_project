using System;
using System.Collections;
using UnityEngine;

public class BaseUtility : MonoBehaviour
{
    public int moneyPerDay;
    public int reputationPerDay;
    public Transform[] locations;
    public GameObject moneyIconPrefab;
    public GameObject reputationIconPrefab;

    public Money money;
    public Reputation reputation;

    private void Start()
    {
        if(money == null)
        {
            money = Money.Instance;
        }
        if(reputation == null)
        {
            reputation = Reputation.Instance;
        }
    }

    private void OnEnable()
    {
        TimeManager.OnNewDay += GetDailyBenefit;
    }
    private void OnDisable()
    {
        TimeManager.OnNewDay -= GetDailyBenefit;
    }
    private void GetDailyBenefit()
    {
        money.AddMoney(moneyPerDay);
        reputation.AddReputation(reputationPerDay);

        StartCoroutine(CreateIcon());
    }

    IEnumerator CreateIcon()
    {
        int idx = UnityEngine.Random.Range(0, locations.Length);
        Instantiate(moneyIconPrefab, locations[idx].position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        Instantiate(reputationIconPrefab, locations[idx].position, Quaternion.identity);
    }
}
