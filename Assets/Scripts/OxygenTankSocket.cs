using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankSocket : Interactable
{
    public override string InteractionString => "Place Oxygen Tank";

    public OxygenTank Tank => tank;
    [SerializeField] private OxygenTank tank;

    private void Awake()
    {
        // Ensure the tank is attached if it starts off that way
        if (tank != null) Attach(tank);
    }

    public override void Interact(Player interactor)
    {
        if (interactor.Hands.Holding is OxygenTank tank)
        {
            interactor.Hands.DropHeld();
            Attach(tank);
        }
    }

    private void Attach(OxygenTank newTank)
    {
        if (tank != null && tank != newTank) { return; }

        newTank.AttachToSocket(transform);
        tank = newTank;

        tank.OnTaken += Detach;
    }

    private void Detach()
    {
        if (tank == null) return;

        tank.OnTaken -= Detach;
        tank = null;
    }
}
