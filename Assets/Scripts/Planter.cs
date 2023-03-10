using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource))]
public class Planter : Interactable
{
    public static Action<bool> OnPlantHarvested;

    public override string InteractionString
    {
        get
        {
            if (!IsFertilized) return "Fertilize";
            if (IsHarvestable) return "Harvest";

            return "Wait 7 Days";
        }
    }

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
    public bool IsHarvestable { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject fertilizerMesh;
    [SerializeField] private Transform plantSocket;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        fertilizerMesh.SetActive(_isFertilized);
    }

    public override void Interact(Player interactor)
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
        if (IsHarvestable && CurrentPlant != null)
        {
            Harvest();

            return;
        }
    }

    public void UpdatePlanterState()
    {
        IsHarvestable = CurrentPlant != null && (CurrentPlant.IsGrown || CurrentPlant.IsDead);
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
        CurrentPlant.IsPlanted = true;
        CurrentPlant.GetComponent<Collider>().enabled = false;

        _audioSource.Play();
    }

    private void Harvest()
    {
        Debug.Log($"Harvested {CurrentPlant.PlantType}");

        OnPlantHarvested?.Invoke(!CurrentPlant.IsDead);

        Destroy(CurrentPlant.gameObject);

        IsHarvestable = false;
        IsFertilized = false;
        CurrentPlant = null;

        _audioSource.Play();
    }
}
