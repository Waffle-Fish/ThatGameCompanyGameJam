using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class FoodDrop
{
    public FoodBehavior Food;
    public float DropChance;

    public FoodDrop(FoodBehavior food, float probablity)
    {
        Food = food;
        DropChance = probablity;
    }
}

[Serializable]
[CreateAssetMenu(fileName = "RandomFoodList", menuName = "RandomizedFoodLists", order = 0)]
public class RandomizedFoodListScriptableObject : ScriptableObject
{
    [Tooltip("If any probablities are < 0.01f, all probabilities will be set to equal")]
    public List<FoodDrop> FoodList;

    private void OnEnable()
    {
        FixProbabilities();
    }

    public void EqualizeChance()
    {
        if (FoodList.Count == 0)
        {
            Debug.LogWarning("Food list has no food");
            return;
        }

        float probablity = 1 / (float)FoodList.Count;
        for (int i = 0; i < FoodList.Count; i++)
        {
            FoodList[i] = new FoodDrop(FoodList[i].Food, probablity);
        }
    }

    public void FixProbabilities()
    {
        if (FoodList.Count == 0)
        {
            Debug.LogWarning("Food list has no food");
            return;
        }

        float totalProbability = 0f;
        if (Mathf.Approximately(totalProbability, 1f)) return;
        foreach (FoodDrop food in FoodList)
        {
            if (food.DropChance < 0.01f)
            {
                EqualizeChance();
                return;
            }
            totalProbability += food.DropChance;
        }

        for (int i = 0; i < FoodList.Count; i++)
        {
            float newProb = FoodList[i].DropChance / totalProbability;
            FoodList[i] = new FoodDrop(FoodList[i].Food, newProb);
        }
    }

    public FoodBehavior RandomlyPickOne()
    {
        float num = UnityEngine.Random.Range(0f, 1f);
        int i = 0;
        float total = 0;
        while (num > total && i < FoodList.Count) {
            total += FoodList[i].DropChance;
            i++;
        } 
        return FoodList[i].Food;
    }
}
