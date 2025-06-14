using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "CustomerScriptableObject", menuName = "CustomerSO", order = 0)]
public class CustomerScriptableObject : ScriptableObject
{
    public Sprite CustomerSprite;
    public Sprite CustomerSpriteBack;
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
        
        if (!RandomizedFoodOrders || RandomizedFoodOrders.FoodList.Count == 0) return;
        for (int i = 0; i < NumRandomFood; i++)
        {
            FoodOrder.Add(RandomizedFoodOrders.RandomlyPickOne());
        }
    }

    public int GetFoodOrderTotal()
    {
        FoodOrderTotalCost = 0;
        if (FoodOrder == null) return 0;
        foreach (var food in FoodOrder)
        {
            FoodOrderTotalCost += food.FoodSO.GetTotalCost();
        }
        return FoodOrderTotalCost;
    }
}
