using UnityEngine;

public class Player : MonoBehaviour
{
    public SpaceSuit SpaceSuit { get; private set; }
    public PlayerHands Hands { get; private set; }
    public bool IsWearingSpaceSuit => SpaceSuit != null;
    public bool IsInSpace { get; private set; }
    public bool IsDead { get; private set; }
    public int Hunger { get; set; } = 0;

    [SerializeField] private GameObject suitHelmet;

    private LayerMask _airMask;

    private void Awake()
    {
        Hands = GetComponentInChildren<PlayerHands>();
        _airMask = LayerMask.GetMask("Air");
    }

    private void Start()
    {
        // Equip a space suit from the beginning
        var spaceSuit = GetComponentInChildren<SpaceSuit>(true);
        if (spaceSuit != null) PutOnSpaceSuit(spaceSuit);
    }

    private void Update()
    {
        if (IsDead) return;

        CheckForAir();

        if (IsInSpace)
        {
            if (IsWearingSpaceSuit)
            {
                if (SpaceSuit.OxygenTank == null || SpaceSuit.OxygenTank.Amount == 0f)
                {
                    Die("Asphyxiation due to lack of oxygen");
                    return;
                }

                SpaceSuit.OxygenTank.ConsumeOxygen(Time.deltaTime);
            }
            else
            {
                Die("Decompression due to exposure to the vacuum of space");
            }
        }
    }

    public void PutOnSpaceSuit(SpaceSuit suit)
    {
        SpaceSuit = suit;
        SpaceSuit.transform.SetParent(transform);
        SpaceSuit.gameObject.SetActive(false);

        suitHelmet.SetActive(true);
    }

    public void TakeOffSpaceSuit()
    {
        SpaceSuit.transform.parent = null;
        SpaceSuit.gameObject.SetActive(true);
        SpaceSuit = null;

        suitHelmet.SetActive(false);
    }

    private void Die(string cause)
    {
        IsDead = true;
        Debug.Log($"YOU ARE DEAD: {cause}");
    }

    private Collider[] _airVolumes = new Collider[1];

    private void CheckForAir()
    {
        Physics.OverlapBoxNonAlloc(suitHelmet.transform.position, Vector3.one * 0.1f, _airVolumes, Quaternion.identity, _airMask, QueryTriggerInteraction.Collide);
        IsInSpace = _airVolumes[0] == null || !_airVolumes[0].GetComponent<AirVolume>().HasAir;
    }
}
