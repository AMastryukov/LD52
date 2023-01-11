using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndingTrigger : MonoBehaviour
{
    public static Action OnEnding;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() != null)
        {
            OnEnding?.Invoke();
            Destroy(gameObject);
        }
    }
}
