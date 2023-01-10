using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public abstract class Holdable : Interactable
{
    public override string InteractionString => "Take";

    public Action OnTaken;

    public Collider Collider { get; private set; }
    public Rigidbody RigidBody { get; private set; }

    [SerializeField] private Vector3 posOffset;
    [SerializeField] private Vector3 rotOffset;

    private AudioSource _audioSource;

    protected virtual void Awake()
    {
        Collider = GetComponent<Collider>();
        RigidBody = GetComponent<Rigidbody>();

        _audioSource = GetComponent<AudioSource>();
    }

    public override void Interact(Player interactor)
    {
        interactor.Hands.PickUp(this);
        OnTaken?.Invoke();

        _audioSource.Play();
    }

    public void AttachToSocket(Transform socket)
    {
        Collider.isTrigger = true;
        RigidBody.isKinematic = true;

        transform.SetParent(socket);
        transform.localPosition = posOffset;
        transform.localRotation = Quaternion.Euler(rotOffset);
    }

    public void DetachFromSocket()
    {
        Collider.isTrigger = false;
        RigidBody.isKinematic = false;
        transform.parent = null;
    }
}
