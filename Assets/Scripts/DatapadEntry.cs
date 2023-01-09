using UnityEngine;
using TMPro;
using System;

public class DatapadEntry : MonoBehaviour
{
    public static Action<VoicenoteData> OnPlayVoicenote;

    public VoicenoteData Data => _data;

    [SerializeField] private TextMeshProUGUI noteText;

    private VoicenoteData _data;

    public void Initialize(VoicenoteData data)
    {
        _data = data;
        noteText.text = _data.note;

        PlayEntry();
    }

    public void PlayEntry()
    {
        OnPlayVoicenote?.Invoke(_data);
    }
}
