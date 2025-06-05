using System;
using System.Collections;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance { get; private set; }
    int currentCustomerIndex = -1;
    CustomerBehavior currentCustomer;
    int revenue = 0;
    
    public static Action<int> OnDespawnCurrentCustomer;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void OnEnable() {
        GameManager.OnSpawnNextCustomer += SpawnNextCustomer;
    }

    private void OnDisable() {
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
        currentCustomerIndex++;
        if (currentCustomerIndex >= transform.childCount)
        {
            Debug.Log("End of day!");
        }
        else
        {
            currentCustomer = transform.GetChild(currentCustomerIndex).GetComponent<CustomerBehavior>();
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
            yield return new WaitForSeconds(5f);
            DespawnCurrentCustomer();
        }

        if (currentCustomer)
        {
            revenue = amountCharged;
            currentCustomer.Pay(amountCharged);
            StartCoroutine(DelayDespawn());
        }
    }
}
