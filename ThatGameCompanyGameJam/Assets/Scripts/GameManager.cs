using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField][Min(1)] private int quota = 0;
    public int DailyQuota { get => quota; private set => quota = value; }
    public int CurrentRevenue { get; private set; } = 0;

    [SerializeField][Min(0f)] float timeBetweenCustomers = 1f;
    float inBetweenCustomerTimer = 0f;
    public static Action OnSpawnNextCustomer;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        ProcessCustomers(0);
    }

    private void OnEnable()
    {
        CustomersManager.OnDespawnCurrentCustomer += ProcessCustomers;
        CustomersManager.OnUpdateCurrentRevenue += UpdateCurrentRevenue;
    }

    private void OnDisable()
    {
        CustomersManager.OnDespawnCurrentCustomer -= ProcessCustomers;
        CustomersManager.OnUpdateCurrentRevenue -= UpdateCurrentRevenue;
    }

    private void ProcessCustomers(int revenue)
    {
        IEnumerator DelaySpawn()
        {
            yield return new WaitForSeconds(timeBetweenCustomers);
            OnSpawnNextCustomer?.Invoke();
        }

        CurrentRevenue += revenue;
        // Update quota UI
        StartCoroutine(DelaySpawn());
    }

    private void UpdateCurrentRevenue(int money)
    {
        CurrentRevenue += money;
    }
}
