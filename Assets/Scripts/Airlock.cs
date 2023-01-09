using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    [SerializeField] private AirVolume airVolume;
    [SerializeField] private Door outsideDoor;
    [SerializeField] private Door insideDoor;

    public void ActivateOutsideDoor()
    {
        if (outsideDoor.IsLocked) return;
        if (!outsideDoor.IsOpen && insideDoor.IsOpen) return;
        if (!outsideDoor.IsOpen && airVolume.HasAir) return;

        Debug.Log($"Outside door closed: {!outsideDoor.IsOpen}, Inside door open: {insideDoor.IsOpen}");

        outsideDoor.Activate();
    }

    public void ActivateInsideDoor()
    {
        if (insideDoor.IsLocked) return;
        if (!insideDoor.IsOpen && outsideDoor.IsOpen) return;
        if (!insideDoor.IsOpen && !airVolume.HasAir) return;

        Debug.Log($"Inside door closed: {!insideDoor.IsOpen}, Outside door open: {outsideDoor.IsOpen}");

        insideDoor.Activate();
    }

    public void ActivateAir()
    {
        if (insideDoor.IsOpen || outsideDoor.IsOpen) return;

        airVolume.Toggle();

        Debug.Log(airVolume.HasAir ? "Added Air to airlock" : "Removed air from airlock");
    }
}
