using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenFabricator : MonoBehaviour
{
    public OxygenTank Tank => _oxygenTankSocket.Tank;
    private OxygenTankSocket _oxygenTankSocket;

    private bool _isRefilling;

    private void Awake()
    {
        _oxygenTankSocket = GetComponentInChildren<OxygenTankSocket>();
    }

    public void Refill()
    {
        if (Tank == null || Tank.IsFull)
        {
            Debug.Log(Tank == null ? "Insert Oxygen Tank" : "Tank is Full");

            Stop();
            return;
        }

        _isRefilling = true;
    }

    private void Stop()
    {
        _isRefilling = false;
    }

    private void Update()
    {
        if (_isRefilling)
        {
            if (Tank == null || Tank.IsFull)
            {
                Debug.Log(Tank == null ? "Insert Oxygen Tank" : "Tank is Full");

                Stop();
                return;
            }

            Tank.AddOxygen(Time.deltaTime * 2f);
            Debug.Log($"Filling Oxygen Tank: {Tank.Amount}");
        }
    }
}
