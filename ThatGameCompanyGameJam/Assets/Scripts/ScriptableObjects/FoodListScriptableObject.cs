using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodListScriptableObject", menuName = "FoodListScriptableObject", order = 0)]
public class FoodListScriptableObject : ScriptableObject
{
    public List<FoodScriptableObject> FoodList;
    [Tooltip("If null or total chance != 100%, will equalize chances of all foods on list")]
    public List<float> FoodChanceTable;

    public void EqualizeChance()
    {
        float probablity = 1 / (float)FoodList.Count;
        for (int i = 0; i < FoodList.Count; i++)
        {
            FoodChanceTable[i] = probablity;
        }
    }

    public bool ValidateFoodChanceTable()
    {
        float totalProbability = 0f;
        foreach (var prob in FoodChanceTable)
        {
            totalProbability += prob;
        }
        return Mathf.Approximately(totalProbability, 1f);
    }
}
