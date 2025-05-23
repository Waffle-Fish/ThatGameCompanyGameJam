using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "RandomFoodList", menuName = "RandomizedFoodLists", order = 0)]
public class RandomizedFoodListScriptableObject : ScriptableObject
{
    [Tooltip("If any probablities are < 0.01f, all probabilities will be set to equal")]
    public List<Tuple<FoodScriptableObject, float>> FoodList = new();

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
            FoodList[i] = Tuple.Create(FoodList[i].Item1, probablity);
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
        foreach (var food in FoodList)
        {
            if (food.Item2 < 0.01f)
            {
                EqualizeChance();
                return;
            }
            totalProbability += food.Item2;
        }

        for (int i = 0; i < FoodList.Count; i++)
        {
            float newProb = FoodList[i].Item2 / totalProbability;
            FoodList[i] = Tuple.Create(FoodList[i].Item1, newProb);
        }
    }

    public FoodScriptableObject RandomlyPickOne()
    {
        float num = UnityEngine.Random.Range(0f, 1f);
        int i = 0;
        float total = 0;
        while (num > total && i < FoodList.Count) {
            total += FoodList[i].Item2;
            i++;
        } 
        return FoodList[i].Item1;
    }
}
