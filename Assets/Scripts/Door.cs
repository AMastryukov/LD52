using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLocked => isLocked;
    public bool IsOpen => isOpen;

    [SerializeField] private bool isLocked;
    [SerializeField] private bool isOpen;
    [SerializeField] private GameObject door;

    private void Start()
    {
        door.SetActive(!isOpen);
    }

    public void Activate()
    {
        if (isOpen) Close();
        else Open();
    }

    private void Open()
    {
        if (IsLocked) return;

        door.SetActive(false);
        isOpen = true;
    }

    private void Close()
    {
        if (IsLocked) return;

        door.SetActive(true);
        isOpen = false;
    }
}
