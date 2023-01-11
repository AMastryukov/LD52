using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VoicenotePlayer : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        DatapadEntry.OnPlayVoicenote += PlayVoiceNote;
        Bed.OnSleep += StopVoiceNote;
        Player.OnDeath += StopVoiceNote;
    }

    private void OnDestroy()
    {
        DatapadEntry.OnPlayVoicenote -= PlayVoiceNote;
        Bed.OnSleep -= StopVoiceNote;
        Player.OnDeath -= StopVoiceNote;
    }

    public void PlayVoiceNote(VoicenoteData data)
    {
        _audioSource.Stop();
        _audioSource.clip = data.voiceClip;
        _audioSource.Play();
    }

    private void StopVoiceNote()
    {
        _audioSource.Stop();
    }

    private void StopVoiceNote(string s)
    {
        StopVoiceNote();
    }
}
