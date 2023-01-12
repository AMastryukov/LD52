using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScreen : MonoBehaviour
{
    private int _currentClicks;
    private float _timer;

    private Canvas _canvas;
    private TimelineManager _timelineManager;
    private Player _player;

    [SerializeField] private Transform[] savePoints;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _timelineManager = FindObjectOfType<TimelineManager>();
        _player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            _currentClicks++;
            _timer = 0.5f;
        }

        if (_currentClicks == 3)
        {
            Toggle();
            _timer = 0f;
        }

        _timer -= Time.deltaTime;
        _timer = Mathf.Max(0f, _timer);

        if (_timer == 0f && _currentClicks > 0)
        {
            _currentClicks = 0;
        }
    }

    private void Toggle()
    {
        if (_canvas.enabled)
        {
            Close();
            return;
        }

        Show();
    }

    private void Show()
    {
        _canvas.enabled = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Close()
    {
        _canvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetDay(int day)
    {
        Teleport(0);
        _timelineManager.SetDay(day);
    }

    public void Teleport(int placeIndex)
    {
        _player.GetComponent<PlayerController>().enabled = false;
        _player.transform.position = savePoints[placeIndex].position;
        _player.transform.rotation = savePoints[placeIndex].rotation;
        _player.GetComponent<PlayerController>().enabled = true;
    }

    public void MaxOxygen()
    {
        if (_player.IsWearingSpaceSuit && _player.SpaceSuit.OxygenTank != null)
        {
            _player.SpaceSuit.OxygenTank.AddOxygen(1000);
        }
    }

    public void MaxFood()
    {
        FindObjectOfType<GameManager>().OverrideFood(100);
    }
}
