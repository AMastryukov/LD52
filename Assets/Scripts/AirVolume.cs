using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AirVolume : MonoBehaviour
{
    public bool HasAir => hasAir;

    public void AddAir() => hasAir = true;
    public void RemoveAir() => hasAir = false;

    [SerializeField] private bool hasAir;

    public void Toggle()
    {
        if (HasAir) RemoveAir();
        else AddAir();
    }
}
