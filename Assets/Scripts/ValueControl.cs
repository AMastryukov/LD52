using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueControl : MonoBehaviour
{
    [Header("Settings")]
    public float min;
    public float max;
    public float delta;
    public float defaultValue;

    public float Current { get; private set; }

    private void Awake()
    {
        Current = Mathf.Clamp(defaultValue, min, max);
    }

    public void Increase()
    {
        Current = Mathf.Clamp(Current + delta, min, max);
    }

    public void Increase(float amount)
    {
        Current = Mathf.Clamp(Current + amount, min, max);
    }

    public void Decrease()
    {
        Current = Mathf.Clamp(Current - delta, min, max);
    }

    public void Decrease(float amount)
    {
        Current = Mathf.Clamp(Current - amount, min, max);
    }
}
