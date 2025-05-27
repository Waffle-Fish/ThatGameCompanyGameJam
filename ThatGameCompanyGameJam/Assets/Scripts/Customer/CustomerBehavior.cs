using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    [SerializeField] CustomerScriptableObject customerSO;
    private SpriteRenderer spriteRenderer;
    private FoodSpawner foodSpawner;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        foodSpawner = FoodSpawner.Instance;

        customerSO.InitializeFoodOrder();
    }

    void Start()
    {
        if (customerSO == null)
        {
            Debug.LogError(name + " has no Customer SO!");
            return;
        }
        // foodSpawner = FoodSpawner.Instance;
    }

    private void OnEnable()
    {
        // if (customerSO != null) DisplayOrder();
        DisplayOrder();
    }

    private void OnDisable() {
        ProcessLeave();
    }

    public void DisplayOrder()
    {
        foreach (var order in customerSO.FoodOrder)
        {
            foodSpawner.PlaceFood(order.FoodSO);
        }
    }

    private void ProcessLeave()
    {
        foodSpawner.ReleaseAllActive();
    }
}
