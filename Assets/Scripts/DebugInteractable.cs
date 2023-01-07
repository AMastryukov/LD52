using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInteractable : Interactable
{
    public override void Interact(PlayerInteractor interactor)
    {
        Debug.Log($"Debug interaction");
    }
}
