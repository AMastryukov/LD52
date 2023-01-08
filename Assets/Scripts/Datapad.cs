using System.Collections.Generic;
using UnityEngine;

public class Datapad : MonoBehaviour
{
    [SerializeField] private DatapadEntry entryPrefab;
    [SerializeField] private Transform datapadEntryContent;

    private Canvas _canvas;
    private List<DatapadEntry> _entries = new();

    private void Awake()
    {
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
    }

    private void Close()
    {
        _canvas.enabled = false;
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
