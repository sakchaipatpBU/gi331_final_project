using TMPro;
using UnityEngine;

public class ReputationUIManager : MonoBehaviour
{
    public Reputation reputation;

    public TMP_Text reputationText;
    public string moneyPrefix = "reputation : ";

    void Start()
    {
        if (reputation == null)
        {
            reputation = Reputation.Instance;
        }

        // update ui ตอนเริ่มเกม
        UpdateReputaionText();
    }

    private void OnEnable()
    {
        Money.OnChangeMoney += UpdateReputaionText;
    }
    private void OnDisable()
    {
        Money.OnChangeMoney -= UpdateReputaionText;
    }
    private void UpdateReputaionText()
    {
        reputationText.text = moneyPrefix + reputation.currentReputation.ToString();
    }
}
