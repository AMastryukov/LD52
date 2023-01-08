using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DustTarget))]
public class SolarPanel : MonoBehaviour
{
    public bool IsEnabled { get; private set; }
    public bool IsDusty => _panel.IsDusty;

    private DustTarget _panel;

    private void Awake()
    {
        _panel = GetComponentInChildren<DustTarget>();
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }
}
