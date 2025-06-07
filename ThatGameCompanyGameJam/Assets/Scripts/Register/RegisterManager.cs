using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RegisterManager : MonoBehaviour
{
    public int CurrentTotal = 0;
    [SerializeField] TextMeshProUGUI tmp;

    bool canChargeCustomer = false;

    private void OnEnable()
    {
        RegisterNumberButton.OnRegisterKeyPressed += ShiftIncreaseTotal;
    }

    private void OnDisable()
    {
        RegisterNumberButton.OnRegisterKeyPressed -= ShiftIncreaseTotal;
    }

    void Start()
    {
        UpdateTotalText();
    }

    public void ShiftIncreaseTotal(int val)
    {
        if (!WithinLimits(CurrentTotal * 10 + val)) return;
        CurrentTotal *= 10;
        CurrentTotal += val;
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

    private bool WithinLimits(int totalToCheck)
    {
        return totalToCheck < 1000000000 && totalToCheck > 0;
    }

    public void SubmitTotal()
    {
        if (canChargeCustomer)
        {
            CustomersManager.Instance.ChargeCurrentCustomer(CurrentTotal);
            canChargeCustomer = false;
        }
        ClearTotal();
    }

    public void SetCanChargeCustomerTrue()
    {
        canChargeCustomer = true;
    }
}
