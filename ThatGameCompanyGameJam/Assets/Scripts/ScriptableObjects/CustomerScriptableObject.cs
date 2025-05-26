using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomerScriptableObject", menuName = "CustomerSO", order = 0)]
public class CustomerScriptableObject : ScriptableObject
{
    public Sprite CustomerSprite;
    public List<FoodScriptableObject> GuranteedFoodOrders;
    public RandomizedFoodListScriptableObject RandomizedFoodOrders;

    [Min(0)]
    public int NumRandomFood;

    public List<FoodScriptableObject> FoodOrder { get; private set; }
    public int FoodOrderTotalCost { get; private set; } = 0;

    public void Awake()
    {
        Debug.Log("Awake");
        FoodOrder = new();
        InitializeFoodOrder();
        GetFoodOrderTotal();
        if (FoodOrder.Count == 0) Debug.LogWarning("CustomerSO: No Food in Customer Order");
    }

    public void OnDestroy()
    {
        Debug.Log("OnDestroy");
        FoodOrder.Clear();
        FoodOrderTotalCost = 0;
    }

    private void InitializeFoodOrder()
    {
        if (GuranteedFoodOrders == null || GuranteedFoodOrders.Count == 0) return;
        if (!RandomizedFoodOrders || RandomizedFoodOrders.FoodList.Count == 0) return;

        FoodOrder.AddRange(GuranteedFoodOrders);
        for (int i = 0; i < NumRandomFood; i++)
        {
            FoodOrder.Add(RandomizedFoodOrders.RandomlyPickOne());
        }
    }

    private void GetFoodOrderTotal()
    {
        FoodOrderTotalCost = 0;
        if (FoodOrder == null) return;
        foreach (var food in FoodOrder)
        {
            FoodOrderTotalCost += food.GetTotalCost();
        }
    }
}
