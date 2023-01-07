using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSuit : MonoBehaviour
{
    private OxygenTankSocket _oxygenTankSocket;
    private OxygenTank _oxygenTank => _oxygenTankSocket.Tank;

    private void Awake()
    {
        _oxygenTankSocket = GetComponentInChildren<OxygenTankSocket>();
    }

    public void TryConsumeOxygen()
    {

    }
}
