using UnityEngine;
using System;

public class TimelineManager : MonoBehaviour
{
    public static Action <int, string> OnDayAdvanced;

    private int[] gameDays = { 0, 1, 7, 14, 21, 24, 31 };
    private int _currentDayIndex = 0;

    public int CurrentDay => gameDays[_currentDayIndex];

    private void AdvanceDay()
    {
        _currentDayIndex++;

        OnDayAdvanced?.Invoke(CurrentDay,"");
    }

    private void Awake()
    {
        Bed.OnSleep += AdvanceDay;
    }

    private void OnDestroy()
    {
        Bed.OnSleep -= AdvanceDay;
    }
}
