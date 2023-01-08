using UnityEngine;

public class SpaceSuitSocket : Interactable
{
    [SerializeField] private SpaceSuit suit;

    private void Start()
    {
        // Ensure the suit is attached if it starts off that way
        if (suit != null) Attach(suit);
    }

    public override void Interact(PlayerInteractor interactor)
    {
        if (interactor.Body.IsWearingSpaceSuit)
        {
            var spaceSuit = interactor.Body.SpaceSuit;

            interactor.Body.TakeOffSpaceSuit();
            Attach(spaceSuit);
        }
    }

    private void Attach(SpaceSuit newSuit)
    {
        // Do not allow multiple suits in the same socket
        if (suit != null && suit != newSuit) { return; }

        // Do parenting manually (SpaceSuit is not a Holdable - bad design)
        newSuit.transform.SetParent(transform);
        newSuit.transform.localPosition = Vector3.zero;
        newSuit.transform.localRotation = Quaternion.identity;

        suit = newSuit;

        suit.OnTaken += Detach;
    }

    private void Detach()
    {
        if (suit == null) return;

        suit.OnTaken -= Detach;
        suit = null;
    }
}
