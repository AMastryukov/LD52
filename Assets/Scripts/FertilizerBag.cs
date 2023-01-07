using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FertilizerBag : Holdable
{
    public int Amount { get; private set; } = 9;

    public void Consume()
    {
        Amount = Mathf.Max(Amount - 1, 0);
        if (Amount == 0) Destroy(gameObject);
    }
}
