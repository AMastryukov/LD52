using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBody : MonoBehaviour
{
    public SpaceSuit SpaceSuit { get; private set; }
    public bool IsWearingSpaceSuit => SpaceSuit != null;

    [SerializeField] private GameObject suitHelmet;

    public void PutOnSpaceSuit(SpaceSuit suit)
    {
        SpaceSuit = suit;
        SpaceSuit.transform.SetParent(transform);
        SpaceSuit.gameObject.SetActive(false);

        suitHelmet.SetActive(true);
    }

    public void TakeOffSpaceSuit()
    {
        SpaceSuit.transform.parent = null;
        SpaceSuit.gameObject.SetActive(true);
        SpaceSuit = null;

        suitHelmet.SetActive(false);
    }


}
