using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planter : Interactable
{
    private bool _isFertilized;
    public bool IsFertilized
    {
        get => _isFertilized;
        private set
        {
            _isFertilized = value;
            fertilizerMesh.SetActive(_isFertilized);
        }
    }
    public Plant CurrentPlant { get; private set; }
    public bool IsReady { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject fertilizerMesh;
    [SerializeField] private Transform plantSocket;

    private void Start()
    {
        fertilizerMesh.SetActive(_isFertilized);
    }

    public override void Interact(PlayerInteractor interactor)
    {
        // If player is holding a fertilizer in their hand, fertilize the planter
        if (!IsFertilized && interactor.Hands.Holding is FertilizerBag fertilizer)
        {
            Fertilize();
            fertilizer.Consume();

            return;
        }

        // If the planter is fertilized without a plant and the player is holding a plant, plant it
        if (IsFertilized && CurrentPlant == null && interactor.Hands.Holding is Plant plant)
        {
            // Drop the plant and immediately plant it
            interactor.Hands.DropHeld();
            Plant(plant);

            return;
        }

        // If the plant is ready, harvest it
        if (IsReady && CurrentPlant != null)
        {
            interactor.Hands.PickUp(CurrentPlant);
            Harvest();

            return;
        }
    }

    private void Fertilize()
    {
        Debug.Log($"Fertilized {name}");

        IsFertilized = true;
    }

    private void Plant(Plant plant)
    {
        Debug.Log($"Planted {plant.PlantType}");

        CurrentPlant = plant;
        CurrentPlant.AttachToSocket(plantSocket);
    }

    private void Harvest()
    {
        Debug.Log($"Harvested {CurrentPlant.PlantType}");

        IsReady = false;
        IsFertilized = false;
        CurrentPlant = null;
    }
}
