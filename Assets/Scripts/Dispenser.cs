using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Dispenser : MonoBehaviour
{
    public int Remaining { get; set; }

    [SerializeField] private GameObject dispenserPrefab;
    [SerializeField] private Transform dispensePoint;
    [SerializeField] private float delay;
    [SerializeField] private int amount;
    [SerializeField] private AudioClip sound;

    private AudioSource _audioSource;
    private bool _isBusy;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Remaining = amount;
    }

    public void Dispense()
    {
        if (_isBusy) return;

        if (dispenserPrefab == null)
        {
            Debug.Log("Dispenser has no assigned object to dispense");
            return;
        }

        if (Remaining == 0)
        {
            Debug.Log("Dispenser is empty");
            return;
        }

        StartCoroutine(DispenseCoroutine());
    }

    private IEnumerator DispenseCoroutine()
    {
        _isBusy = true;

        if (sound != null)
        {
            _audioSource.clip = sound;
            _audioSource.Play();
        }

        yield return new WaitForSeconds(delay);

        Instantiate(dispenserPrefab, dispensePoint);
        Remaining--;

        _isBusy = false;
    }
}
