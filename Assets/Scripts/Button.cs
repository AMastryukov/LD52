using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Interactable
{
    [SerializeField] private UnityEvent action;

    public override void Interact(PlayerInteractor interactor)
    {
        action?.Invoke();
    }
}
