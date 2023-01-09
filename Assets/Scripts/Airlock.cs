using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airlock : MonoBehaviour
{
    public HabitatLights Lights => lights;

    [SerializeField] private AirVolume airVolume;
    [SerializeField] private HabitatLights lights;
    [SerializeField] private Door outsideDoor;
    [SerializeField] private Door insideDoor;
    [SerializeField] private AirlockControl airlockUI;

    private void Start()
    {
        airlockUI.UpdateUI(insideDoor.IsLocked);
    }

    public void ActivateOutsideDoor()
    {
        if (outsideDoor.IsLocked) return;
        if (!outsideDoor.IsOpen && insideDoor.IsOpen) return;
        if (!outsideDoor.IsOpen && airVolume.HasAir) return;

        //Debug.Log($"Outside door closed: {!outsideDoor.IsOpen}, Inside door open: {insideDoor.IsOpen}");

        outsideDoor.Activate();
    }

    public void ActivateInsideDoor()
    {
        if (insideDoor.IsLocked) return;
        if (!insideDoor.IsOpen && outsideDoor.IsOpen) return;
        if (!insideDoor.IsOpen && !airVolume.HasAir) return;

        //Debug.Log($"Inside door closed: {!insideDoor.IsOpen}, Outside door open: {outsideDoor.IsOpen}");

        insideDoor.Activate();
    }

    public void ActivateAir()
    {
        if (insideDoor.IsOpen || outsideDoor.IsOpen) return;

        airVolume.Toggle();

        Debug.Log(airVolume.HasAir ? "Added Air to airlock" : "Removed air from airlock");

        airlockUI.UpdateUI(airVolume.HasAir);
    }

    public void Lock()
    {
        outsideDoor.IsLocked = true;
        insideDoor.IsLocked = true;
    }

    public void Unlock()
    {
        outsideDoor.IsLocked = false;
        insideDoor.IsLocked = false;
    }
}
