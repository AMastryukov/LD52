using UnityEngine;

public class HabitatLights : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private Light[] backupLights;

    public void TurnOn()
    {
        foreach (var light in backupLights)
        {
            light.enabled = false;
        }

        foreach (var light in lights)
        {
            light.enabled = true;
        }
    }

    public void TurnOff()
    {
        foreach (var light in backupLights)
        {
            light.enabled = true;
        }

        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }
}
