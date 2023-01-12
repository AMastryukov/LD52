using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PowerState))]
[RequireComponent(typeof(DustTarget))]
public class SolarPanel : DustTarget
{
    public override string InteractionString => IsDusty ? "Dust Off" : "Solar Panel";

    public PowerState Power => _power;

    private PowerState _power;

    private void Awake()
    {
        _power = GetComponent<PowerState>();
    }
}
