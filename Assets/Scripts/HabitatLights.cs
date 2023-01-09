using UnityEngine;

public class HabitatLights : MonoBehaviour
{
    [SerializeField] private Light[] regularLights;
    [SerializeField] private Light[] backupLights;

    public void TurnOnRegularLights()
    {
        foreach (var light in backupLights)
        {
            light.enabled = false;
        }

        foreach (var light in regularLights)
        {
            light.enabled = true;
        }
    }

    public void TurnOnBackupLights()
    {
        foreach (var light in backupLights)
        {
            light.enabled = true;
        }

        foreach (var light in regularLights)
        {
            light.enabled = false;
        }
    }
}
