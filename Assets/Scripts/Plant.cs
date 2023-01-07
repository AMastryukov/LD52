using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : Holdable
{
    public enum Type { A, B, C }

    public Type PlantType => plantType;
    [SerializeField] private Type plantType = Type.A;
}
