using System.Collections;
using UnityEngine;
using TMPro;

public class SleepOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI subText;

    [SerializeField] private Canvas nextDayOverlay;

    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();

        TimelineManager.OnDayAdvanced += ShowOverlay;
    }

    private void OnDestroy()
    {
        TimelineManager.OnDayAdvanced -= ShowOverlay;
    }

    private void ShowOverlay(int from, int to)
    {
        StartCoroutine(OverlayScreenCoroutine(from, to));
    }

    private IEnumerator OverlayScreenCoroutine(int from, int to)
    {
        if (player != null) player.GoToSleep();

        mainText.text = $"Day {from}";
        subText.text = "";

        nextDayOverlay.enabled = true;

        yield return new WaitForSeconds(2);

        mainText.text = $"Day {to}";

        yield return new WaitForSeconds(1);

        subText.text = $"({to - from} day(s) later)";

        yield return new WaitForSeconds(2);

        nextDayOverlay.enabled = false;

        if (player != null) player.WakeUp();
    }

}
