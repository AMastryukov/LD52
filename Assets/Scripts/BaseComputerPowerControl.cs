using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseComputerPowerControl : MonoBehaviour
{
    private Color onColor = new Color(0.2739287f, 0.5647059f, 0.2470588f);
    private Color offColor = new Color(0.5660378f, 0.248309f, 0.248309f);

    [Header("References")]
    [SerializeField] private Habitat habitat;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void UpdateControl()
    {
        var status = habitat.NoPower ? "<color=red>[INSUFFICIENT POWER]</color>" : "<color=green>[CONNECTED]</color>";
        statusText.text = $"{habitat.name}\n<size=14>{status}</size>";
        button.image.color = habitat.Power.Online ? onColor : offColor;
        buttonText.text = habitat.Power.Online ? "ON" : "OFF";
    }

    public void ToggleState()
    {
        // Prevent the player from turning it has no power
        if (habitat.NoPower) return;

        // Toggle power state
        habitat.Power.Online = !habitat.Power.Online;
        UpdateControl();
    }
}
