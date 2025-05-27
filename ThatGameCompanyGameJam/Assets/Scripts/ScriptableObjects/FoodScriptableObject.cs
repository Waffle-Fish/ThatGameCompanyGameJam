using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultFood", menuName = "FoodSO", order = 0)]
public class FoodScriptableObject : ScriptableObject
{
    public Sprite Sprite;
    public List<IngredientScriptableObject> Ingredients;

    public int GetTotalCost()
    {
        int totalCost = 0;
        foreach (var ing in Ingredients)
        {
            totalCost += ing.Cost;
        }
        return totalCost;
    }
}
