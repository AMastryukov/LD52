using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueControl : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float min;
    [SerializeField] private float max;
    [SerializeField] private float delta;
    [SerializeField] private float defaultValue;

    public float Current { get; private set; }

    private void Awake()
    {
        Current = defaultValue;
    }

    public void Increase()
    {
        Current = Mathf.Clamp(Current + delta, min, max);
    }

    public void Decrease()
    {
        Current = Mathf.Clamp(Current - delta, min, max);
    }
}
