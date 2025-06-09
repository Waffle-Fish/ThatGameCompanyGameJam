using UnityEngine;


public enum EndingTypes {Good, Corpo, Neutral, Bad}
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }

    int GoodEndingRequirement;
    int CorpoEndingRequirement;

    public int GoodEndingCounter { get; private set; }
    public int CorpoEndingCounter { get; private set; }
    public int MissedQuotas { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this) Destroy(this);
        else Instance = this;
        DontDestroyOnLoad(this);

        GoodEndingCounter = 0;
        CorpoEndingCounter = 0;
        MissedQuotas = 0;

        GoodEndingRequirement = 200;
        CorpoEndingRequirement = 200;
    }

    public void IncreaseGoodEndingCounter(int increment)
    {
        Debug.Log("Increasing GE Counter");
        GoodEndingCounter += increment;
    }

    public void IncreaseCorpoEndingCounter(int increment)
    {
        Debug.Log("Increasing CE Counter");
        CorpoEndingCounter += increment;
    }

    public void IncreaseMissedQuota()
    {
        MissedQuotas++;
    }

    public EndingTypes GetEndingType()
    {
        if (MissedQuotas >= 2) return EndingTypes.Bad;
        else if (GoodEndingCounter >= GoodEndingRequirement) return EndingTypes.Good;
        else if (CorpoEndingCounter >= CorpoEndingRequirement) return EndingTypes.Corpo;
        else return EndingTypes.Neutral;
    }

    public void ClearData()
    {
        GoodEndingCounter = 0;
        CorpoEndingCounter = 0;
        MissedQuotas = 0;
    }
}
