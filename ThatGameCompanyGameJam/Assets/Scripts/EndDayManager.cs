using TMPro;
using UnityEngine;

public class EndDayManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI quotaTMP;
    [SerializeField] TextMeshProUGUI underChargeTMP;
    [SerializeField] TextMeshProUGUI overChargeTMP;

    private void OnEnable()
    {
        CustomersManager.AllCustomersServed += UpdateEndText;
    }

    private void OnDisable() {
        CustomersManager.AllCustomersServed -= UpdateEndText;
    }

    public void UpdateEndText()
    {
        string metQuota = (GameManager.Instance.CurrentRevenue > GameManager.Instance.DailyQuota) ? " met " : " did not meet";
        quotaTMP.text = $"You {metQuota} the quota";
        underChargeTMP.text = $"You undercharged {PlayerDataManager.Instance.NumPplUnderCharged} people";
        overChargeTMP.text = $"You overcharged {PlayerDataManager.Instance.NumPplOverCharged} people";
    }
}
