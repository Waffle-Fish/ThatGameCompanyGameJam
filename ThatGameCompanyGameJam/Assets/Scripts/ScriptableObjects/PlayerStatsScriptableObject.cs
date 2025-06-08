using UnityEngine;

public struct DayInfo
{
    public int Day;
    public bool MetQuota;
    public int Revenue;
    public int NumOvercharged;
    public int NumUndercharged;
}

[CreateAssetMenu(fileName = "WeeklyInfo", menuName = "WeeklyInfos", order = 0)]
public class PlayerStatsScriptableObject : ScriptableObject
{
    public DayInfo[] Days = new DayInfo[3];
}
