using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsLocked => isLocked;

    [SerializeField] private bool isLocked;
    [SerializeField] private GameObject door;

    private bool _isOpen;

    private void Start()
    {
        door.SetActive(!_isOpen);
    }

    public void Activate()
    {
        if (IsLocked)
        {
            Debug.Log("Door is locked");
            return;
        }

        if (_isOpen) Close();
        else Open();
    }

    private void Open()
    {
        door.SetActive(false);
        _isOpen = true;
    }

    private void Close()
    {
        door.SetActive(true);
        _isOpen = false;
    }
}
