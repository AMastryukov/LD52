using UnityEngine;
using System.Collections;
using DG.Tweening;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    public bool IsLocked { get; set; }
    public bool IsOpen => isOpen;

    [SerializeField] private bool isOpen;
    [SerializeField] private bool isSilent;
    [SerializeField] private Transform door;
    [SerializeField] private Transform openTransform;
    [SerializeField] private Transform closeTransform;

    private bool _isBusy;
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public IEnumerator OpenDoor()
    {
        if (IsLocked || _isBusy || IsOpen) yield break;

        _isBusy = true;

        if (!isSilent) _audioSource.Play();

        door.transform.DOMove(openTransform.position, 2f);
        yield return new WaitForSeconds(2f);

        _isBusy = false;
        isOpen = true;
    }

    public IEnumerator CloseDoor()
    {
        if (IsLocked || _isBusy || !IsOpen) yield break;

        _isBusy = true;

        if (!isSilent) _audioSource.Play();

        door.transform.DOMove(closeTransform.position, 2f);
        yield return new WaitForSeconds(2f);

        _isBusy = false;
        isOpen = false;
    }
}
