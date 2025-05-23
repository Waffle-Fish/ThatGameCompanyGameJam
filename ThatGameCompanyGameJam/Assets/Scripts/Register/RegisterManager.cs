using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RegisterManager : MonoBehaviour
{
    public uint CurrentTotal = 0;
    [SerializeField] TextMeshProUGUI tmp;

    private void OnEnable()
    {
        RegisterNumberButton.OnRegisterKeyPressed += ShiftIncreaseTotal;
    }

    void Start()
    {
        UpdateTotalText();
    }

    public void ShiftIncreaseTotal(int val)
    {
        if (!WithinLimits(CurrentTotal * 10 + (uint)val)) return;
        CurrentTotal *= 10;
        CurrentTotal += (uint)val;
        UpdateTotalText();
    }

    public void ShiftDecreaseTotal()
    {
        CurrentTotal /= 10;
        UpdateTotalText();
    }

    public void IncrementTotal()
    {
        if (!WithinLimits(CurrentTotal + 1)) return;
        CurrentTotal++;
        UpdateTotalText();
    }

    public void DecrementTotal()
    {
        if (!WithinLimits(CurrentTotal - 1)) return;
        CurrentTotal--;
        UpdateTotalText();
    }

    public void ClearTotal()
    {
        CurrentTotal = 0;
        UpdateTotalText();
    }

    private void UpdateTotalText()
    {
        tmp.text = CurrentTotal.ToString();
    }

    private bool WithinLimits(uint totalToCheck)
    {
        return totalToCheck < 1000000000 && totalToCheck > 0;
    }
}
