using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
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

    private void OnDrawGizmos()
    {
        if (!HasAir) return;

        var collider = GetComponent<BoxCollider>();

        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawCube(collider.transform.position + collider.center, collider.size);
    }
}
