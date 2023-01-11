using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class DeathScreen : MonoBehaviour
{
    public static Action OnRestart;

    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI causeOfDeath;
    [SerializeField] private Button restartButton;

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;

    private Sequence deathSequence;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();

        Player.OnDeath += ShowScreen;
    }

    private void OnDestroy()
    {
        Player.OnDeath -= ShowScreen;
    }

    private void ShowScreen(string cause)
    {
        _canvas.enabled = true;
        _canvasGroup.alpha = 0f;

        restartButton.gameObject.SetActive(false);

        causeOfDeath.text = $"Cause of death: {cause}";
        causeOfDeath.alpha = 0f;
        title.alpha = 0f;

        deathSequence = DOTween.Sequence();
        deathSequence.Append(_canvasGroup.DOFade(1f, 2f));
        deathSequence.Append(title.DOFade(1f, 2f));
        deathSequence.Append(causeOfDeath.DOFade(1f, 2f));
        deathSequence.OnComplete(() =>
        {
            Cursor.lockState = CursorLockMode.Confined;
            restartButton.gameObject.SetActive(true);
        });
    }

    public void Restart()
    {
        var gameScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(gameScene.name);
    }
}
