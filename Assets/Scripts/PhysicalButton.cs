using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : Interactable
{
    public override string InteractionString => customInteractionText;

    [SerializeField] private UnityEvent action;
    [SerializeField] private string customInteractionText = "Interact";

    public override void Interact(Player interactor)
    {
        action?.Invoke();
    }
}
