using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AirlockControl : MonoBehaviour
{
    private Color onColor = new Color(0.2739287f, 0.5647059f, 0.2470588f);
    private Color offColor = new Color(0.5660378f, 0.248309f, 0.248309f);

    [SerializeField] private TextMeshProUGUI topText;
    [SerializeField] private Image buttonImage;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void UpdateUI(bool hasAir)
    {
        if (hasAir)
        {
            topText.text = "<b>PRESSURIZED</b>\n\n interior door unlocked";
            buttonText.text = "Depressurize";
            buttonImage.color = offColor;
        }
        else
        {

            topText.text = "<b>DEPRESSURIZED</b>\n\n exterior door unlocked";
            buttonText.text = "Pressurize";
            buttonImage.color = onColor;
        }
    }
}
