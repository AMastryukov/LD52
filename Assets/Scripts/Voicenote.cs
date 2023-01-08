using UnityEngine;
using System;

public class Voicenote : Interactable
{
    [SerializeField] private VoicenoteData data;

    public static Action<VoicenoteData> OnVoiceNotePickedUp;

    public override void Interact(Player interactor)
    {
        OnVoiceNotePickedUp?.Invoke(data);
        Destroy(gameObject);
    }
}
