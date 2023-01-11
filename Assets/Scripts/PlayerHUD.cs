using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    private string[] hungerStrings =
    {
        "",
        "Hungry",
        "Starving",
        "Malnourished",
        "Seriously Malnourished",
        "Critically Malnourished"
    };

    [SerializeField] private Canvas interactionCanvas;
    [SerializeField] private Canvas spacesuitCanvas;
    [SerializeField] private Canvas closeCanvas;

    [SerializeField] private Image oxygenImage;

    [SerializeField] private TextMeshProUGUI oxygenText;
    [SerializeField] private TextMeshProUGUI interactionText;
    [SerializeField] private TextMeshProUGUI hungerText;

    [SerializeField] private TextMeshProUGUI subtitleText;

    private Player _player;
    private PlayerController _controller;
    private PlayerInteractor _interactor;
    private SpaceSuit SpaceSuit => _player.SpaceSuit;
    private Interactable LookingAt => _interactor.LookingAt;

    private RectTransform _interactionRect;

    private Coroutine _subCoroutine;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        _controller = FindObjectOfType<PlayerController>();
        _interactor = FindObjectOfType<PlayerInteractor>();

        _interactionRect = interactionCanvas.GetComponent<RectTransform>();

        Voicenote.OnVoiceNotePickedUp += ShowSubtitle;
        DatapadEntry.OnPlayVoicenote += ShowSubtitle;
        Bed.OnSleep += StopSubtitles;
    }

    private void OnDestroy()
    {
        Voicenote.OnVoiceNotePickedUp -= ShowSubtitle;
        DatapadEntry.OnPlayVoicenote -= ShowSubtitle;
        Bed.OnSleep -= StopSubtitles;
    }

    private void Start()
    {
        subtitleText.text = "";
    }

    private void Update()
    {
        UpdateInteractionCanvas();
        UpdateSpacesuitCanvas();
        UpdateCloseCanvas();
        UpdateHungerCanvas();
    }

    private void UpdateInteractionCanvas()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_interactionRect);

        interactionCanvas.enabled = _interactor.enabled && LookingAt != null;
        if (LookingAt == null) return;

        interactionText.text = LookingAt.InteractionString;
    }

    private void UpdateSpacesuitCanvas()
    {
        spacesuitCanvas.enabled = SpaceSuit != null;
        if (SpaceSuit == null) return;

        oxygenText.text = string.Format("{0:0}", SpaceSuit.OxygenTank.Fraction * 100f);
        oxygenImage.fillAmount = SpaceSuit.OxygenTank.Fraction;
    }

    private void UpdateCloseCanvas()
    {
        closeCanvas.enabled = _controller.CurrentState == PlayerController.State.Computer;
    }

    private void UpdateHungerCanvas()
    {
        hungerText.text = hungerStrings[_player.Hunger];
    }

    private void ShowSubtitle(VoicenoteData data)
    {
        StopSubtitles();
        _subCoroutine = StartCoroutine(ShowSubtitleCoroutine(data));
    }

    private void StopSubtitles()
    {
        if (_subCoroutine != null)
        {
            StopCoroutine(_subCoroutine);
            _subCoroutine = null;

            subtitleText.text = "";
        }
    }

    private IEnumerator ShowSubtitleCoroutine(VoicenoteData data)
    {
        subtitleText.text = data.subtitle;
        yield return new WaitForSeconds(data.voiceClip.length);
        subtitleText.text = "";
    }
}
