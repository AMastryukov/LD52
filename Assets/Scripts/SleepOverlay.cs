using System.Collections;
using UnityEngine;
using TMPro;

public class SleepOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI daysUntilHarvestText;

    [SerializeField] private Canvas nextDayOverlay;

    private void Awake()
    {
        TimelineManager.OnDayAdvanced += ShowOverlay;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= ShowOverlay;
    }
    
    private void ShowOverlay(int day, string text)
    {
        StartCoroutine(OverlayScreenCoroutine());
        dayText.text = "Day " + day;
        daysUntilHarvestText.text = "Days until harvest text: " + text;
    }

    private IEnumerator OverlayScreenCoroutine()
    {
        nextDayOverlay.enabled = true;
        yield return new WaitForSeconds(5);
        nextDayOverlay.enabled = false;
    }

}
