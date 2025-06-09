using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField][Min(1)] private int quota = 0;
    public int DailyQuota { get => quota; private set => quota = value; }
    public int CurrentRevenue { get; private set; } = 0;

    [SerializeField][Min(0f)] float timeBetweenCustomers = 1f;
    float inBetweenCustomerTimer = 0f;
    public static Action OnSpawnNextCustomer;
    bool hasStartedGame = false;

    [SerializeField] PlayableDirector endPD;

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
    }

    private void OnEnable()
    {
        CustomersManager.OnDespawnCurrentCustomer += ProcessCustomers;
        CustomersManager.OnUpdateCurrentRevenue += UpdateCurrentRevenue;
        CustomersManager.AllCustomersServed += EndGame;
    }

    private void OnDisable()
    {
        CustomersManager.OnDespawnCurrentCustomer -= ProcessCustomers;
        CustomersManager.OnUpdateCurrentRevenue -= UpdateCurrentRevenue;
        CustomersManager.AllCustomersServed -= EndGame;
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

    public void StartGame()
    {
        if (hasStartedGame) return;
        ProcessCustomers(0);
        AudioManager.Main.StartMusic();
        hasStartedGame = true;
        Debug.Log("The game has started!");
    }

    private void EndGame()
    {
        endPD.Play();
    }
}
