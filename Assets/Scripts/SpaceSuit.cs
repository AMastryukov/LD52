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

    public override void Interact(PlayerInteractor interactor)
    {
        interactor.Body.PutOnSpaceSuit(this);
        OnTaken?.Invoke();
    }

    public void ConsumeOxygen()
    {

    }
}