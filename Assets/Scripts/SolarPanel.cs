using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerState))]
[RequireComponent(typeof(DustTarget))]
public class SolarPanel : MonoBehaviour
{
    public PowerState Power => _power;
    public DustTarget Dust => _panel;
    public bool IsDusty => _panel.IsDusty;

    private PowerState _power;
    private DustTarget _panel;

    private void Awake()
    {
        _power = GetComponent<PowerState>();
        _panel = GetComponent<DustTarget>();
    }
}
