using UnityEngine;
using TMPro;

public class ValueDisplay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ValueControl control;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Settings")]
    [SerializeField] private string format;
    [SerializeField] private string suffix;

    private string _formattedValue;

    private void Update()
    {
        _formattedValue = string.IsNullOrWhiteSpace(format) ? control.Current.ToString() : string.Format(format, control.Current);
        text.text = $"{_formattedValue}{suffix}";
    }
}
