using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomerScriptableObject", menuName = "FoodListScriptableObject", order = 0)]
public class CustomerScriptableObject : ScriptableObject
{
    public Sprite Sprite;
    public List<FoodScriptableObject> GuranteedFoodOrders;
    public RandomizedFoodListScriptableObject RandomizedFoodOrders;

    [Min(0)]
    public int NumRandomFood;

    public List<FoodScriptableObject> FoodOrder { get; private set; } = new();
    public int FoodOrderTotalCost { get; private set; } = 0;

    public void Awake()
    {
        Debug.Log("Awake");
        InitializeFoodOrder();
        GetFoodOrderTotal();
        if (FoodOrder.Count == 0) Debug.LogWarning("No Food in Customer Order");
    }

    public void OnDestroy()
    {
        Debug.Log("OnDestroy");
        FoodOrder.Clear();
        FoodOrderTotalCost = 0;
    }

    private void InitializeFoodOrder()
    {
        FoodOrder.AddRange(GuranteedFoodOrders);
        for (int i = 0; i < NumRandomFood; i++)
        {
            FoodOrder.Add(RandomizedFoodOrders.RandomlyPickOne());
        }
    }

    private void GetFoodOrderTotal()
    {
        FoodOrderTotalCost = 0;
        foreach (var food in FoodOrder)
        {
            FoodOrderTotalCost += food.GetTotalCost();
        }
    }
}
