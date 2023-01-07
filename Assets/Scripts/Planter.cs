using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Interactable
{
    public enum Plant { None = -1, A, B, C }

    [SerializeField] private Plant currentPlant = Plant.None;

    public override void Interact(PlayerInteractor interactor)
    {
        if (currentPlant != Plant.None) return;
        currentPlant = Plant.A;
    }
}
