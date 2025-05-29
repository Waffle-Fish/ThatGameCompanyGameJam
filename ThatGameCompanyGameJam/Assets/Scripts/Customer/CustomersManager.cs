using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    public static CustomersManager Instance { get; private set; }
    int currentCustomerIndex = -1;
    CustomerBehavior currentCustomer;

    private void Awake() {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
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
        try
        {
            currentCustomerIndex++;
            currentCustomer = transform.GetChild(currentCustomerIndex).GetComponent<CustomerBehavior>();
            currentCustomer.gameObject.SetActive(true);
            // currentCustomer.GetComponent<Animator>
        }
        catch (System.Exception)
        {
            Debug.LogError("Trying to activate a non-existent child - customerIndex: " + currentCustomerIndex);
            throw;
        }
    }

    public void DespawnCurrentCustomer()
    {
        StartCoroutine(currentCustomer.ProcessExit());
        currentCustomer = null;
    }

    public void ChargeCurrentCustomer(int amountCharged)
    {
        currentCustomer.Pay(amountCharged);
    }
}
