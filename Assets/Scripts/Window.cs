using UnityEngine;

[RequireComponent(typeof(DustTarget))]
public class Window : Interactable
{
    public DustTarget DustTarget => _dust;
    public bool IsClosed { get; private set; }
    public bool IsDusty => _dust.IsDusty;

    [SerializeField] private GameObject shutters;

    private DustTarget _dust;

    private void Awake()
    {
        _dust = GetComponent<DustTarget>();
    }

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
        if (IsClosed) OpenShutters();
        else CloseShutters();
    }
}
