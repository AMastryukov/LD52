using System;

public class Bed : Interactable
{
    public static Action OnSleep;

    public override void Interact(Player interactor)
    {
        //TODO disable player movement
        OnSleep?.Invoke();
    }
}
