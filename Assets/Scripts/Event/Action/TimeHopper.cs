using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New TimeHopper", menuName = "TimeHopper")]
public class TimeHopper : EventAction
{
    public enum TimeHopType
    {
        Hour,
        TimeOfDay,
        Day,
        Week,
    }

    public TimeHopType HopType;
    public int Day;
    public WorldTime.TimeOfDay TimeOfDay;
    public float Hour;
    public WorldTime.WeekDay WeekDay;

    public override void Execute()
    {
        switch (HopType)
        {
            case TimeHopType.Hour:
                WorldTime.Instance.SetTime(Hour);
                break;
            case TimeHopType.TimeOfDay:
                WorldTime.Instance.SetTime(TimeOfDay);
                break;
            case TimeHopType.Day:
                WorldTime.Instance.SetTime(Day);
                break;
            case TimeHopType.Week:
                WorldTime.Instance.SetTime(WeekDay);
                break;
        }
    }
}
