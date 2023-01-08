using UnityEngine;

/// <summary>
/// Represents the power state of a habitat or whatever
/// Set this stuff in GameManager
/// </summary>
public class PowerState : MonoBehaviour
{
    public bool Online { get; set; }
    public bool IsLocked { get; set; } = true; // Can it be toggled on/off?
    public string Status { get; set; } // "Days until online: 20" or "Online" something like that
}
