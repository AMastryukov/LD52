using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceSuit : Interactable
{
    public override string InteractionString => "Put on";

    public Action OnTaken;

    public OxygenTank OxygenTank => oxygenTankSocket.Tank;
    [SerializeField] private OxygenTankSocket oxygenTankSocket;

    public override void Interact(Player interactor)
    {
        // Edge case where the player is holding an oxygen tank and wants to attach it but misses the socket collider
        if (interactor.Hands.Holding is OxygenTank tank && OxygenTank == null)
        {
            oxygenTankSocket.Interact(interactor);
            return;
        }

        interactor.PutOnSpaceSuit(this);
        OnTaken?.Invoke();
    }
}