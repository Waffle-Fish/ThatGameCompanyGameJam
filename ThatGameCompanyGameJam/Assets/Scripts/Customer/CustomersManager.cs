using UnityEngine;

public class CustomersManager : MonoBehaviour
{
    int currentCustomerIndex = -1;

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
            transform.GetChild(currentCustomerIndex).gameObject.SetActive(true);
        }
        catch (System.Exception)
        {
            Debug.LogError("Trying to activate a non-existent child - customerIndex: " + currentCustomerIndex);
            throw;
        }
    }

    public void DespawnCurrentCustomer()
    {
        transform.GetChild(currentCustomerIndex).gameObject.SetActive(false);
    }
}
