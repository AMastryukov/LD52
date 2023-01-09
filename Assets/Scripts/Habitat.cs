using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitat : MonoBehaviour
{
    public PowerState Power => power;
    public Airlock Airlock => airlock;

    [SerializeField] private PowerState power;
    [SerializeField] private HabitatLights lights;
    [SerializeField] private Airlock airlock;

    private void Awake()
    {
        power.OnStateChanged += UpdatePowerState;
    }

    private void OnDestroy()
    {
        power.OnStateChanged -= UpdatePowerState;
    }

    private void UpdatePowerState()
    {
        if (power.Online)
        {
            lights.TurnOn();
            airlock.Lights.TurnOn();
        }
        else
        {
            lights.TurnOff();
            airlock.Lights.TurnOff();
        }
    }
}
