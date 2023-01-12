using System.Collections;
using UnityEngine;
using TMPro;

public class SleepOverlay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI mainText;
    [SerializeField] private TextMeshProUGUI consumeText;
    [SerializeField] private TextMeshProUGUI remainingText;
    [SerializeField] private TextMeshProUGUI tipText;

    [SerializeField] private Canvas nextDayOverlay;

    private Player player;
    private GameManager gameManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        gameManager = FindObjectOfType<GameManager>();

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

        mainText.text = "";
        consumeText.text = "";
        remainingText.text = "";
        tipText.text = "";

        nextDayOverlay.enabled = true;

        yield return new WaitForSeconds(2);

        for (int i = from; i <= to; i++)
        {
            mainText.text = $"Day {i}";
            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(1);

        consumeText.text = $"Consumed {to - from} food from reserves.";

        yield return new WaitForSeconds(2);

        remainingText.text = $"{gameManager.FoodReserves} day(s) of food remaining";

        yield return new WaitForSeconds(2);

        if (player.IsWearingSpaceSuit)
        {
            tipText.text = "Tip: don't go to sleep in your space suit, conserve your oxygen.";
        }
        else if (gameManager.Day == 1)
        {
            tipText.text = "Tip: go to Habitat B and plant something for next week.";
        }
        else if (gameManager.FoodReserves < 5)
        {
            tipText.text = "Tip: food reserves are low, remember to plant & harvest every week.";
        }
        else
        {
            tipText.text = "Tip: check the base computer to see habitat status.";
        }

        yield return new WaitForSeconds(3);

        nextDayOverlay.enabled = false;

        if (player != null) player.WakeUp();
    }

}
