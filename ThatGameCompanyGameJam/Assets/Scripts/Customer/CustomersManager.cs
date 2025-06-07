using System;
using System.Collections;
using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    [SerializeField][Min(0f)] Vector2 AcceptablePurchaseRange = Vector2.one * 0.5f;
    public static CustomersManager Instance { get; private set; }
    public int CurrentCustomerIndex { get; private set; } = -1;
    CustomerBehavior currentCustomer;
    int revenue = 0;

    public static Action<int> OnDespawnCurrentCustomer;

    public static Action<int> OnUpdateCurrentRevenue;

    bool processingDespawn = false;

    RegisterManager registerManager;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        registerManager = FindFirstObjectByType<RegisterManager>(FindObjectsInactive.Include);
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
            int amountChargedResults = IsAmountChargedTooExtreme(amountCharged);
            Debug.Log("Amount charged results: " + amountChargedResults);
            if (amountChargedResults != 0)
            {
                // Play inner monologue
                if (amountChargedResults == -1)
                {
                    Debug.Log("I can't give them that big of a discount! The manager will kill me!");
                }
                else
                {
                    Debug.Log("That's too much of an over charge! The customer will leave!");
                }
            }
            else
            {
                revenue = amountCharged;
                registerManager.SetCanChargeCustomer(false);
                currentCustomer.Pay(amountCharged);
                UpdateCurrentRevenue(amountCharged);
                StartCoroutine(DelayDespawn());
            }
        }
    }


    public void UpdateCurrentRevenue(int amountCharged)
    {
        OnUpdateCurrentRevenue?.Invoke(amountCharged);
    }

    private int IsAmountChargedTooExtreme(int amountCharged)
    {
        int total = currentCustomer.GetFoodOrderTotal();
        int floor = (int)Mathf.Clamp(total - AcceptablePurchaseRange.x * total, 0f, 10000000f);
        int ceil = (int)Mathf.Clamp(total + AcceptablePurchaseRange.y * total, 0f, 10000000f);
        if (amountCharged < floor) return -1;
        else if (amountCharged > ceil) return 1;
        else return 0;
    }
}
