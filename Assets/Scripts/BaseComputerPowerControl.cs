using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BaseComputerPowerControl : MonoBehaviour
{
    public PowerState Control => powerState;

    private Color onColor = new Color(0.2739287f, 0.5647059f, 0.2470588f);
    private Color offColor = new Color(0.5660378f, 0.248309f, 0.248309f);

    [Header("References")]
    [SerializeField] private PowerState powerState;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void UpdateControl()
    {
        statusText.text = powerState.Status;
        button.image.color = powerState.Online ? onColor : offColor;
        buttonText.text = powerState.Online ? "ON" : "OFF";
    }

    public void ToggleState()
    {
        if (powerState == null || powerState.IsLocked) return;

        powerState.Online = !powerState.Online;
        UpdateControl();
    }
}
