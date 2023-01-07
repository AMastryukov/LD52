using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class Holdable : Interactable
{
    public Rigidbody RigidBody { get; private set; }

    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotOffset;

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    public override void Interact(PlayerInteractor interactor)
    {
        interactor.Hands.PickUp(this);
    }

    public void AttachToSocket(Transform socket)
    {
        RigidBody.isKinematic = true;

        transform.SetParent(socket);
        transform.localPosition = posOffset;
        transform.localRotation = Quaternion.Euler(rotOffset);
    }

    public void DetachFromSocket()
    {
        RigidBody.isKinematic = false;
        transform.parent = null;
    }
}
