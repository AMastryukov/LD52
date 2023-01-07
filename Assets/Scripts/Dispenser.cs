using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform dispensePoint;

    private bool _onCooldown;

    public void Dispense()
    {
        if (prefab == null)
        {
            Debug.Log("Dispenser has no assigned object to dispense");
            return;
        }

        if (_onCooldown) return;

        Instantiate(prefab, dispensePoint);
        _onCooldown = true;
    }

    public void ResetDispenser()
    {
        _onCooldown = false;
    }
}
