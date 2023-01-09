using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DustTarget))]
public class Window : MonoBehaviour
{
    public DustTarget DustTarget => _dust;
    public bool IsClosed { get; private set; }

    [SerializeField] private GameObject shutters;

    private DustTarget _dust;

    private void Awake()
    {
        _dust = GetComponent<DustTarget>();
    }

    public void Open()
    {
        IsClosed = false;
    }

    public void Close()
    {
        IsClosed = true;
    }
}
