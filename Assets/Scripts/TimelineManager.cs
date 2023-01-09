using UnityEngine;
using System;

public class TimelineManager : MonoBehaviour
{
    public static Action<int, int> OnDayAdvanced;

    private int[] _days = { 0, 1, 7, 14, 21, 24, 31 };
    private int _currentIndex = 0;

    public int CurrentDay => _days[_currentIndex];

    private void AdvanceDay()
    {
        if (_currentIndex == _days.Length - 1) return;

        _currentIndex++;
        OnDayAdvanced?.Invoke(_days[_currentIndex - 1], CurrentDay);
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
