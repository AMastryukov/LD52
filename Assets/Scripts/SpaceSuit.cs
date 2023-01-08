using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceSuit : Interactable
{
    public Action OnTaken;

    public OxygenTank OxygenTank => _oxygenTankSocket.Tank;
    private OxygenTankSocket _oxygenTankSocket;

    private void Awake()
    {
        _oxygenTankSocket = GetComponentInChildren<OxygenTankSocket>();
    }

    public override void Interact(Player interactor)
    {
        // Edge case where the player is holding an oxygen tank and wants to attach it but misses the socket collider
        if (interactor.Hands.Holding is OxygenTank tank && OxygenTank == null)
        {
            _oxygenTankSocket.Interact(interactor);
            return;
        }

        interactor.PutOnSpaceSuit(this);
        OnTaken?.Invoke();
    }
}