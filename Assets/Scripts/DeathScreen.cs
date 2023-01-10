using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public static Action OnRestart;

    [SerializeField] private TextMeshProUGUI causeOfDeath;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();

        Player.OnDeath += ShowScreen;
    }

    private void OnDestroy()
    {
        Player.OnDeath -= ShowScreen;
    }

    private void ShowScreen(string cause)
    {
        _canvas.enabled = true;

        causeOfDeath.text = $"Cause of death: {cause}";
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Restart()
    {
        var gameScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(gameScene.name);
    }
}
