using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSocket<T> : MonoBehaviour where T : Interactable
{
    public T Object { get; private set; }

    public void Attach(T obj)
    {
        Object = obj;
    }

    public void Detach()
    {
        Object = null;
    }
}
