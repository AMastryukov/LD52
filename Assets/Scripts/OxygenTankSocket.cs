using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenTankSocket : Interactable
{
    public OxygenTank Tank => tank;
    [SerializeField] private OxygenTank tank;

    private void Start()
    {
        // Ensure the tank is attached if it starts off that way
        if (Tank != null) Attach(Tank);
    }

    public override void Interact(PlayerInteractor interactor)
    {
        if (interactor.Hands.Holding is OxygenTank tank) Attach(tank);
    }

    private void Attach(OxygenTank newTank)
    {
        // If a tank is already attached, detach it
        if (Tank != null) { Detach(); }

        newTank.AttachToSocket(transform);
        tank = newTank;
    }

    private void Detach()
    {
        if (Tank == null) return;

        Tank.DetachFromSocket();
        tank = null;
    }
}
