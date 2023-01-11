using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class IntroScreen : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private CanvasGroup quoteCG;

    Sequence sequence;

    public void ShowIntro()
    {
        var playerController = FindObjectOfType<PlayerController>();
        playerController.enabled = false;

        mainCanvas.enabled = true;
        mainCanvasGroup.alpha = 1f;
        quoteCG.alpha = 0f;

        sequence = DOTween.Sequence();
        sequence.AppendInterval(3f);
        sequence.Append(quoteCG.DOFade(1f, 3f));
        sequence.AppendInterval(3f);
        sequence.Append(quoteCG.DOFade(0f, 3f));
        sequence.AppendInterval(2f);
        sequence.Append(mainCanvasGroup.DOFade(0f, 3f));
        sequence.OnComplete(() =>
        {
            mainCanvas.enabled = false;
            playerController.enabled = true;

            playerController.GetComponent<Player>().SpaceSuit.OxygenTank.AddOxygen(100f);
        });
    }
}
