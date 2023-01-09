using UnityEngine;
using System;

/// <summary>
/// Represents the power state of a habitat or whatever
/// Set this stuff in GameManager
/// </summary>
public class PowerState : MonoBehaviour
{
    public Action OnStateChanged;

    private bool _isOnline;
    public bool Online
    {
        get => _isOnline;
        set
        {
            _isOnline = value;
            OnStateChanged?.Invoke();
        }
    }


    private bool _isLocked;
    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            OnStateChanged?.Invoke();
        }
    }
}
