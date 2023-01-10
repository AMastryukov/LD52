using UnityEngine;
using System;

public class Voicenote : Interactable
{
    public override string InteractionString => "Take Voicenote";

    [SerializeField] private VoicenoteData data;

    public static Action<VoicenoteData> OnVoiceNotePickedUp;

    public override void Interact(Player interactor)
    {
        OnVoiceNotePickedUp?.Invoke(data);
        Destroy(gameObject);
    }
}
