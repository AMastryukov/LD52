using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AirVolume : MonoBehaviour
{
    public bool HasAir { get; private set; } = true;

    public void AddAir() => HasAir = true;
    public void RemoveAir() => HasAir = false;

    private void OnDrawGizmos()
    {
        var collider = GetComponent<BoxCollider>();

        Gizmos.color = new Color(1f, 1f, 1f, 0.2f);
        Gizmos.DrawCube(collider.transform.position + collider.center, collider.size);
    }
}
