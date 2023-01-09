using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Datapad : MonoBehaviour
{
    [SerializeField] private DatapadEntry entryPrefab;
    [SerializeField] private Transform datapadEntryContent;

    [SerializeField] private TextMeshProUGUI dayText;
    [SerializeField] private TextMeshProUGUI foodText;

    private GameManager _gameManager;
    private Canvas _canvas;
    private List<DatapadEntry> _entries = new();

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _canvas = GetComponent<Canvas>();

        PlayerController.OnOpenDatapad += Open;
        PlayerController.OnCloseDatapad += Close;
        Voicenote.OnVoiceNotePickedUp += AddEntry;
    }

    private void OnDestroy()
    {
        PlayerController.OnOpenDatapad -= Open;
        PlayerController.OnCloseDatapad -= Close;
        Voicenote.OnVoiceNotePickedUp -= AddEntry;
    }

    private void Open()
    {
        _canvas.enabled = true;
        UpdateUI();
    }

    private void Close()
    {
        _canvas.enabled = false;
    }

    private void UpdateUI()
    {
        dayText.text = $"Day {_gameManager.Day}";
        foodText.text = $"Food Reserves: {_gameManager.FoodReserves}";
    }

    private void AddEntry(VoicenoteData data)
    {
        // If same note is picked up, don't add it (we already have it)
        foreach (var entry in _entries)
        {
            if (entry.Data == data) return;
        }

        var newEntry = Instantiate(entryPrefab, datapadEntryContent);
        newEntry.Initialize(data);

        _entries.Add(newEntry);
    }
}
