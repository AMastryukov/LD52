using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ValueControl))]
public class OxygenTank : Holdable
{
    private ValueControl _oxygen;

    protected override void Awake()
    {
        base.Awake();
        _oxygen = GetComponent<ValueControl>();
    }

    public void AddOxygen()
    {
        _oxygen.Increase();
    }

    public void RemoveOxygen()
    {
        _oxygen.Decrease();
    }
}
