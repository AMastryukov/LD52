using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustTarget : Interactable
{
    public bool IsDusty
    {
        get => isDusty;
        private set
        {
            isDusty = value;
            dust.SetActive(isDusty);
        }
    }

    [SerializeField] private bool isDusty;
    [SerializeField] private GameObject dust;

    private void Start()
    {
        dust.SetActive(IsDusty);
    }

    public override void Interact(Player interactor)
    {
        if (IsDusty) Clean();
    }

    public void AddDust() => IsDusty = true;
    public void Clean() => IsDusty = false;
}
