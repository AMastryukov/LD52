using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerDispenser : MonoBehaviour
{
    [SerializeField] private FertilizerBag fertilizerPrefab;
    [SerializeField] private Transform dispensePoint;

    private bool _onCooldown;

    public void Dispense()
    {
        if (_onCooldown) return;

        Instantiate(fertilizerPrefab, dispensePoint);
        _onCooldown = true;
    }

    public void ResetDispenser()
    {
        _onCooldown = false;
    }
}
