using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValueControl))]
public class OxygenTank : Holdable
{
    public override string InteractionString => "Take Oxygen Tank";

    public float Amount => _oxygen.Current;
    public float Fraction => _oxygen.Current / _oxygen.max;
    public bool IsFull => Amount == _oxygen.max;

    private ValueControl _oxygen;

    protected override void Awake()
    {
        base.Awake();
        _oxygen = GetComponent<ValueControl>();
    }

    public void AddOxygen(float amount)
    {
        if (_oxygen.Current < _oxygen.max) _oxygen.Increase(amount);
    }

    public void ConsumeOxygen(float amount)
    {
        if (_oxygen.Current > _oxygen.min) _oxygen.Decrease(amount);
    }
}
