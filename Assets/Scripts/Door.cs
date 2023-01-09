using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLocked => isLocked;
    public bool IsOpen => _isOpen;

    [SerializeField] private bool isLocked;
    [SerializeField] private GameObject door;

    private bool _isOpen;

    private void Start()
    {
        door.SetActive(!_isOpen);
    }

    public void Activate()
    {
        if (_isOpen) Close();
        else Open();
    }

    private void Open()
    {
        if (IsLocked) return;

        door.SetActive(false);
        _isOpen = true;
    }

    private void Close()
    {
        if (IsLocked) return;

        door.SetActive(true);
        _isOpen = false;
    }
}
