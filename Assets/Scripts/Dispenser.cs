using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dispenser : MonoBehaviour
{
    public int Remaining { get; set; }

    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform dispensePoint;
    [SerializeField] private int amount;

    private void Start()
    {
        Remaining = amount;
    }

    public void Dispense()
    {
        if (prefab == null)
        {
            Debug.Log("Dispenser has no assigned object to dispense");
            return;
        }

        if (Remaining == 0)
        {
            Debug.Log("Dispenser is empty");
            return;
        }

        Instantiate(prefab, dispensePoint);
        Remaining--;
    }
}
