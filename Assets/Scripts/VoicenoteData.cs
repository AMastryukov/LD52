using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Voicenote", menuName = "Voicenote")]
public class VoicenoteData : ScriptableObject
{
    public string note;
    public AudioClip voiceClip;
    public string subtitle;
}
