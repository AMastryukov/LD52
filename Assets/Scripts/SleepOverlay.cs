using System.Collections;
using UnityEngine;
using TMPro;

public class SleepOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI subText;

    [SerializeField] private Canvas nextDayOverlay;

    // TODO: map days to text entries

    private void Awake()
    {
        TimelineManager.OnDayAdvanced += ShowOverlay;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= ShowOverlay;
    }

    private void ShowOverlay(int from, int to)
    {
        StartCoroutine(OverlayScreenCoroutine());
        mainText.text = "Day " + to;
        subText.text = $"({to - from} day(s) later)";
    }

    private IEnumerator OverlayScreenCoroutine()
    {
        nextDayOverlay.enabled = true;
        yield return new WaitForSeconds(5);
        nextDayOverlay.enabled = false;
    }

}
