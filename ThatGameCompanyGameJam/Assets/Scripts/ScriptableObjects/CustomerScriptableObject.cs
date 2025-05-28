using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomerScriptableObject", menuName = "CustomerSO", order = 0)]
public class CustomerScriptableObject : ScriptableObject
{
    public Sprite CustomerSprite;
    public List<FoodBehavior> GuranteedFoodOrders;
    public RandomizedFoodListScriptableObject RandomizedFoodOrders;

    [Min(0)]
    public int NumRandomFood;

    public List<FoodBehavior> FoodOrder { get; private set; }
    public int FoodOrderTotalCost { get; private set; } = 0;

    public void OnDestroy()
    {
        Debug.Log("OnDestroy");
        FoodOrder.Clear();
        FoodOrderTotalCost = 0;
    }

    public void InitializeFoodOrder()
    {
        if (GuranteedFoodOrders == null || GuranteedFoodOrders.Count == 0) return;
        FoodOrder = new();
        FoodOrder.AddRange(GuranteedFoodOrders);
        
        // IGNORE RANDOM FOOD FOR NOW
        // if (!RandomizedFoodOrders || RandomizedFoodOrders.FoodList.Count == 0) return;
        // for (int i = 0; i < NumRandomFood; i++)
        // {
        //     FoodOrder.Add(RandomizedFoodOrders.RandomlyPickOne());
        // }
    }

    public void GetFoodOrderTotal()
    {
        FoodOrderTotalCost = 0;
        if (FoodOrder == null) return;
        foreach (var food in FoodOrder)
        {
            FoodOrderTotalCost += food.FoodSO.GetTotalCost();
        }
    }
}
