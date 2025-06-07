using System;
using System.Collections;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance { get; private set; }
    public int CurrentCustomerIndex { get; private set; } = -1;
    CustomerBehavior currentCustomer;
    int revenue = 0;

    public static Action<int> OnDespawnCurrentCustomer;

    public static Action<int> OnUpdateCurrentRevenue;

    bool processingDespawn = false;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void OnEnable()
    {
        GameManager.OnSpawnNextCustomer += SpawnNextCustomer;
    }

    private void OnDisable()
    {
        GameManager.OnSpawnNextCustomer -= SpawnNextCustomer;
    }

    void Start()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void SpawnNextCustomer()
    {
        CurrentCustomerIndex++;
        if (CurrentCustomerIndex >= transform.childCount)
        {
            Debug.Log("End of day!");
        }
        else
        {
            currentCustomer = transform.GetChild(CurrentCustomerIndex).GetComponent<CustomerBehavior>();
            currentCustomer.gameObject.SetActive(true);
        }
    }

    public void DespawnCurrentCustomer()
    {
        StartCoroutine(currentCustomer.ProcessExit());
        currentCustomer = null;
        OnDespawnCurrentCustomer?.Invoke(revenue);
        revenue = 0;
    }

    public void ChargeCurrentCustomer(int amountCharged)
    {
        IEnumerator DelayDespawn()
        {
            processingDespawn = true;
            yield return new WaitForSeconds(5f);
            DespawnCurrentCustomer();
            processingDespawn = false;
        }

        if (currentCustomer && !processingDespawn)
        {
            revenue = amountCharged;
            currentCustomer.Pay(amountCharged);
            UpdateCurrentRevenue(amountCharged);
            StartCoroutine(DelayDespawn());
        }
    }
    
    
    public void UpdateCurrentRevenue(int amountCharged)
    {
        OnUpdateCurrentRevenue?.Invoke(amountCharged);
    } 
}
