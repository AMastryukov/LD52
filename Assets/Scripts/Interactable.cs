using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Interactable : MonoBehaviour
{
    public virtual string InteractionString => "Interact";
    public abstract void Interact(Player interactor);
}
