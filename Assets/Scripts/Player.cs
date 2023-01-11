using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public static Action<string> OnDeath;

    public SpaceSuit SpaceSuit { get; private set; }
    public PlayerHands Hands { get; private set; }
    public bool IsWearingSpaceSuit => SpaceSuit != null;
    public bool IsInSpace { get; private set; }
    public bool IsDead { get; private set; }
    public int Hunger { get; set; } = 0;

    [SerializeField] private GameObject suitHelmet;
    [SerializeField] private AudioClip putOnSuit;
    [SerializeField] private AudioClip takeOffSuit;

    private LayerMask _airMask;
    private PlayerController _controller;
    private AudioSource _audioSource;

    private void Awake()
    {
        Hands = GetComponentInChildren<PlayerHands>();
        _airMask = LayerMask.GetMask("Air");

        _controller = GetComponent<PlayerController>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // Equip a space suit from the beginning
        var spaceSuit = GetComponentInChildren<SpaceSuit>(true);
        if (spaceSuit != null) PutOnSpaceSuit(spaceSuit, true);
    }

    private void Update()
    {
        if (IsDead) return;

        CheckForAir();

        // Drain spacesuit oxygen
        if (IsWearingSpaceSuit) SpaceSuit.OxygenTank.ConsumeOxygen(Time.deltaTime);

        if (IsInSpace)
        {
            if (IsWearingSpaceSuit && SpaceSuit.OxygenTank.Amount == 0f) Die("Asphyxiation due to lack of oxygen");
            else if (!IsWearingSpaceSuit) Die("Decompression due to exposure to the vacuum of space");
        }
    }

    public void PutOnSpaceSuit(SpaceSuit suit, bool silent = false)
    {
        SpaceSuit = suit;
        SpaceSuit.transform.SetParent(transform);
        SpaceSuit.gameObject.SetActive(false);

        _audioSource.Stop();
        suitHelmet.SetActive(true);

        if (!silent) _audioSource.PlayOneShot(putOnSuit);
        _audioSource.Play();
    }

    public void TakeOffSpaceSuit()
    {
        SpaceSuit.transform.parent = null;
        SpaceSuit.gameObject.SetActive(true);
        SpaceSuit = null;

        suitHelmet.SetActive(false);

        _audioSource.Stop();
        _audioSource.PlayOneShot(takeOffSuit);
    }

    public void GoToSleep()
    {
        _controller.TrySleep();
    }

    public void WakeUp()
    {
        _controller.WakeUp();
    }

    public void Kill(string cause)
    {
        Die(cause);
    }

    private void Die(string cause)
    {
        _audioSource.Stop();
        _audioSource.volume = 0f;

        IsDead = true;
        OnDeath?.Invoke(cause);
    }

    private Collider[] _airVolumes = new Collider[1];

    private void CheckForAir()
    {
        Physics.OverlapBoxNonAlloc(suitHelmet.transform.position, Vector3.one * 0.1f, _airVolumes, Quaternion.identity, _airMask, QueryTriggerInteraction.Collide);
        IsInSpace = _airVolumes[0] == null || !_airVolumes[0].GetComponent<AirVolume>().HasAir;
    }
}
