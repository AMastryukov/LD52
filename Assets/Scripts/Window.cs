using UnityEngine;

public class Window : DustTarget
{
    public override string InteractionString => IsDusty ? "Dust Off" : "Close/Open Window";

    public bool IsClosed { get; private set; }

    [SerializeField] private GameObject shutters;

    private void Start()
    {
        OpenShutters();
    }

    public void OpenShutters()
    {
        IsClosed = false;
        shutters.SetActive(false);
    }

    public void CloseShutters()
    {
        IsClosed = true;
        shutters.SetActive(true);
    }

    public override void Interact(Player interactor)
    {
        if (IsDusty)
        {
            Clean();
            return;
        }

        if (IsClosed) OpenShutters();
        else CloseShutters();
    }
}
