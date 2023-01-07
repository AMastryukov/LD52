using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Interactable
{
    public enum Plant { None = -1, A, B, C }

    [SerializeField] private bool isFertilized = false;
    [SerializeField] private Plant currentPlant = Plant.None;

    public override void Interact(PlayerInteractor interactor)
    {
        // If player is holding a fertilizer in their hand, fertilize the planter
        if (!isFertilized && interactor.Hands.Holding is FertilizerBag fertilizer)
        {
            isFertilized = true;
            fertilizer.Use();

            return;
        }

        // If the planter is fertilized without a plant and the player is holding a plant, plant it
        if (isFertilized && currentPlant == Plant.None)
        {
            // TODO: Add different plants
            currentPlant = Plant.A;
        }
    }
}
