using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerBag : Holdable
{
    public int Amount => amount;
    [SerializeField] private int amount = 9;

    public void Consume()
    {
        amount = Mathf.Max(Amount - 1, 0);
        if (Amount == 0) Destroy(gameObject);
    }
}
