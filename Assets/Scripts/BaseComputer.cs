using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BaseComputer : Interactable
{
    public override string InteractionString => "Use Computer";

    public string WarningText { get; set; }
    public string BroadcastText { get; set; }

    [Header("Warning")]
    [SerializeField] private GameObject warningPanel;
    [SerializeField] private TextMeshProUGUI warningText;

    [Header("Power State Panels")]
    [SerializeField] private BaseComputerPowerControl powerControl;
    [SerializeField] private BaseComputerPowerControl habitatBControl;
    [SerializeField] private BaseComputerPowerControl habitatCControl;

    [Header("Broadcast")]
    [SerializeField] private GameObject broadcastControlPanel;
    [SerializeField] private TMP_InputField messageInputField;
    [SerializeField] private Button broadcastButton;
    [SerializeField] private GameObject postBroadcastControlPanel;
    [SerializeField] private TextMeshProUGUI postBroadcastText;

    [Header("View")]
    [SerializeField] private Transform viewPosition;

    private PlayerController _controller;
    private bool _isFocused = false;

    private void Awake()
    {
        PlayerController.OnStopUsingComputer += Unfocus;
    }

    private void OnDestroy()
    {
        PlayerController.OnStopUsingComputer -= Unfocus;
    }

    // Call this in GameManager every day the player wakes up
    // Set WarningText, and PowerState values beforehand!
    public void Refresh()
    {
        // Update Warning
        warningPanel.SetActive(!string.IsNullOrEmpty(WarningText));
        warningText.text = WarningText;

        // Update power controls
        powerControl.UpdateControl();
        habitatBControl.UpdateControl();
        habitatCControl.UpdateControl();
    }

    public void BroadcastMessage()
    {
        if (string.IsNullOrEmpty(messageInputField.text)) return;

        // Set the broadcast message from the input field
        BroadcastText = messageInputField.text;
        postBroadcastText.text = $"<b>Broadcasting:</b> {BroadcastText}...";

        broadcastControlPanel.SetActive(false);
        postBroadcastControlPanel.SetActive(true);
    }

    public override void Interact(Player interactor)
    {
        if (!_isFocused) UseComputer(interactor);
    }

    private void UseComputer(Player player)
    {
        _controller = player.GetComponent<PlayerController>();
        if (_controller.TryUseComputer(viewPosition)) _isFocused = true;
    }

    private void Unfocus()
    {
        _isFocused = false;

        // FIX: Unfocuses the input text so it stops taking input from the keyboard
        EventSystem.current.SetSelectedGameObject(null);
    }
}
