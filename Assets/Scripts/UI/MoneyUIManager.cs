using System;
using TMPro;
using UnityEngine;

public class MoneyUIManager : MonoBehaviour
{
    public Money money;

    public TMP_Text moneyText;
    public string moneyPrefix = "Money : ";

    void Start()
    {
        if (money == null)
        {
            money = Money.Instance;
        }

        // update ui ตอนเริ่มเกม
        UpdateMoneyText();
    }

    private void OnEnable()
    {
        Money.OnChangeMoney += UpdateMoneyText;
    }
    private void OnDisable()
    {
        Money.OnChangeMoney -= UpdateMoneyText;
    }
    private void UpdateMoneyText()
    {
        moneyText.text = moneyPrefix + money.currentMoney.ToString();
    }
}
