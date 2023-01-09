using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SolarPanelControl : MonoBehaviour
{
    private Color onColor = new Color(0.2739287f, 0.5647059f, 0.2470588f);
    private Color offColor = new Color(0.5660378f, 0.248309f, 0.248309f);

    public bool Online { get; private set; }

    [SerializeField] private SolarPanel[] solarArray;

    [Header("Screen")]
    [SerializeField] private Image statusImage;
    [SerializeField] private TextMeshProUGUI statusText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    private void Start()
    {
        Online = true;

        foreach (var panel in solarArray)
        {
            if (!panel.Power.Online) Online = false;
        }
    }

    public void Toggle()
    {
        if (Online) Disable();
        else Enable();
    }

    public void Disable()
    {
        Online = false;

        statusImage.color = offColor;
        statusText.text = "OFFLINE";

        buttonImage.color = onColor;
        buttonText.text = "RESET";

        foreach (var panel in solarArray)
        {
            panel.Power.Online = false;
        }
    }

    public void Enable()
    {
        Online = true;

        statusImage.color = offColor;
        statusText.text = "ONLINE";

        buttonImage.color = onColor;
        buttonText.text = "TURN OFF";

        foreach (var panel in solarArray)
        {
            panel.Power.Online = true;
        }
    }
}
