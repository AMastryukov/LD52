using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class OxygenFabricator : MonoBehaviour
{
    public OxygenTank Tank => _oxygenTankSocket.Tank;
    private OxygenTankSocket _oxygenTankSocket;

    private bool _isRefilling;

    private AudioSource _audioSource;

    private void Awake()
    {
        _oxygenTankSocket = GetComponentInChildren<OxygenTankSocket>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.volume = 0f;
    }

    public void Refill()
    {
        if (Tank == null || Tank.IsFull)
        {
            Stop();
            return;
        }

        _isRefilling = true;
        _audioSource.Play();
    }

    private void Stop()
    {
        _isRefilling = false;
        _audioSource.Stop();
    }

    private void Update()
    {
        if (_isRefilling)
        {
            if (Tank == null || Tank.IsFull)
            {
                Stop();
                return;
            }

            Tank.AddOxygen(Time.deltaTime * 50f);

            _audioSource.pitch = 1f + Tank.Fraction;
        }
    }
}
