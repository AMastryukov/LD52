using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SpaceSuit SpaceSuit { get; private set; }
    public bool IsWearingSpaceSuit => SpaceSuit != null;
    public bool IsInSpace => false;
    public bool IsDead { get; private set; }

    [SerializeField] private GameObject suitHelmet;

    private void Update()
    {
        if (IsWearingSpaceSuit)
        {
            SpaceSuit.OxygenTank.ConsumeOxygen(Time.deltaTime);
            Debug.Log($"Current Oxygen Level: {SpaceSuit.OxygenTank.Amount}");
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
}
