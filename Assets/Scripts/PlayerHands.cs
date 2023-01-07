using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to hold an item
/// </summary>
public class PlayerHands : MonoBehaviour
{
    public Holdable Holding { get; private set; }

    [SerializeField] private Transform holdingSocket;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) DropHeld();
    }

    public void PickUp(Holdable holdable)
    {
        DropHeld();

        // Disable rigidbody
        holdable.RigidBody.isKinematic = true;

        // Attach the holdable to the socket
        holdable.AttachToSocket(holdingSocket);

        Holding = holdable;
    }

    public void DropHeld()
    {
        if (Holding == null) return;

        // Detach the holdable from the socket
        Holding.DetachFromSocket();
        Holding.RigidBody.AddForce(transform.forward * 3f, ForceMode.Impulse);
        Holding = null;
    }
}
