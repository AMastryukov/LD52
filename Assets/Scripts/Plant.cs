using UnityEngine;

public class Plant : Holdable
{
    public enum Type { A, B, C }

    public Type PlantType => plantType;
    public bool IsGrown { get; private set; }
    public bool IsDead { get; private set; }

    [Header("References")]
    [SerializeField] private GameObject aliveMesh;
    [SerializeField] private GameObject deadMesh;

    [Header("Values")]
    [SerializeField] private Type plantType = Type.A;
    [SerializeField] private float optimalTemperature;
    [SerializeField] private int optimalPressure;

    public void TryGrow(float temperature, int pressure)
    {
        var tempDiff = Mathf.Abs(optimalTemperature - temperature);
        var presDiff = Mathf.Abs(optimalPressure - pressure);

        // Check if the conditions were right for it to grow
        IsDead = (tempDiff > 1f || presDiff > 4);
        if (!IsDead) IsGrown = true;

        // Toggle alive/dead meshes
        aliveMesh.SetActive(!IsDead);
        deadMesh.SetActive(IsDead);

        // Increase the size of the grown mesh since it's grown
        if (IsGrown)
        {
            aliveMesh.transform.localScale = Vector3.one * 3f;
        }
    }
}
