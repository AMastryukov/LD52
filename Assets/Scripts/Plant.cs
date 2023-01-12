using UnityEngine;

public class Plant : Holdable
{
    public enum Type { A, B, C }

    public Type PlantType => plantType;
    public bool IsGrown { get; private set; }
    public bool IsDead { get; private set; }
    public bool IsPlanted { get; set; }

    [Header("References")]
    [SerializeField] private GameObject aliveMesh;
    [SerializeField] private GameObject deadMesh;

    [Header("Values")]
    [SerializeField] private Type plantType = Type.A;
    [SerializeField] private float optimalTemperature;
    [SerializeField] private int optimalPressure;

    public void TryGrow(float temperature, float pressure, bool hasLight, bool hasPower)
    {
        if (IsDead) return;

        // Check if the conditions were right for it to grow
        if (optimalTemperature != temperature || optimalPressure != pressure || !hasLight || !hasPower) Kill();
        else ForceGrow();
    }

    public override void Interact(Player interactor)
    {
        if (IsDead)
        {
            Destroy(gameObject);
            return;
        }

        base.Interact(interactor);
    }

    public void ForceGrow()
    {
        IsDead = false;
        IsGrown = true;

        // Toggle alive/dead meshes
        aliveMesh.SetActive(true);
        deadMesh.SetActive(false);

        aliveMesh.transform.localScale = Vector3.one * 3f;
    }

    public void Kill()
    {
        IsDead = true;
        IsGrown = false;

        // Toggle alive/dead meshes
        aliveMesh.SetActive(false);
        deadMesh.SetActive(true);

        deadMesh.transform.localScale = Vector3.one * 0.5f;
    }
}
