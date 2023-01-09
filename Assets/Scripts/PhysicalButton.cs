using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PhysicalButton : Interactable
{
    [SerializeField] private UnityEvent action;

    public override void Interact(Player interactor)
    {
        action?.Invoke();
    }
}
