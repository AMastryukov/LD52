using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Allows the player to hold an item
/// </summary>
public class PlayerHands : MonoBehaviour
{
    public Holdable Holding { get; private set; }

    private void Awake()
    {
        FertilizerBag.OnConsumed += DeleteHeld;
    }

    private void OnDestroy()
    {
        FertilizerBag.OnConsumed -= DeleteHeld;
    }

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
        holdable.AttachToSocket(transform);

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

    private void DeleteHeld(FertilizerBag bag)
    {
        if (bag == Holding)
        {
            Destroy(Holding.gameObject);
            Holding = null;
        }
    }
}
