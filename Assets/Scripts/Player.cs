using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpaceSuit SpaceSuit { get; private set; }
    public PlayerHands Hands { get; private set; }
    public bool IsWearingSpaceSuit => SpaceSuit != null;
    public bool IsInSpace { get; private set; }
    public bool IsDead { get; private set; }

    [SerializeField] private GameObject suitHelmet;

    private LayerMask _airMask;

    private void Awake()
    {
        Hands = GetComponentInChildren<PlayerHands>();
        _airMask = LayerMask.GetMask("Air");
    }

    private void Start()
    {
        // Equip a space suit from the beginning
        var spaceSuit = GetComponentInChildren<SpaceSuit>(true);
        if (spaceSuit != null) PutOnSpaceSuit(spaceSuit);
    }

    private void Update()
    {
        if (IsDead) return;

        CheckForAir();

        if (IsWearingSpaceSuit && SpaceSuit.OxygenTank != null)
        {
            SpaceSuit.OxygenTank.ConsumeOxygen(Time.deltaTime);
            // Debug.Log($"Current Oxygen Level: {SpaceSuit.OxygenTank.Amount}");
        }

        if (IsInSpace)
        {
            if (IsWearingSpaceSuit)
            {
                if (SpaceSuit.OxygenTank == null || SpaceSuit.OxygenTank.Amount == 0f)
                {
                    Die("Asphyxiation due to lack of oxygen");
                }
            }
            else
            {
                Die("Asphyxiation & decompression due to exposure to the vacuum of space");
            }
        }
    }

    public void PutOnSpaceSuit(SpaceSuit suit)
    {
        SpaceSuit = suit;
        SpaceSuit.transform.SetParent(transform);
        SpaceSuit.gameObject.SetActive(false);

        suitHelmet.SetActive(true);
    }

    public void TakeOffSpaceSuit()
    {
        SpaceSuit.transform.parent = null;
        SpaceSuit.gameObject.SetActive(true);
        SpaceSuit = null;

        suitHelmet.SetActive(false);
    }

    private void Die(string cause)
    {
        IsDead = true;
        Debug.Log($"YOU ARE DEAD: {cause}");
    }

    private void CheckForAir()
    {
        IsInSpace = !Physics.CheckBox(suitHelmet.transform.position, Vector3.one * 0.1f, Quaternion.identity, _airMask, QueryTriggerInteraction.Collide);
    }
}
