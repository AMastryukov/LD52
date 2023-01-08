using UnityEngine;

[RequireComponent(typeof(PlayerHands))]
public class PlayerInteractor : MonoBehaviour
{
    public Interactable LookingAt { get; private set; }
    public Player Player { get; private set; }

    [Header("Settings")]
    [SerializeField] private float interactionDistance;

    private LayerMask _mask;
    private RaycastHit _hit;

    private void Awake()
    {
        // Lock the cursor (UI interactions are done separately)
        Cursor.lockState = CursorLockMode.Locked;

        // Ignore Player layer
        _mask = ~LayerMask.GetMask("Player");

        Player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        CastLookingRay();

        if (Input.GetKeyDown(KeyCode.E)) CastInteractionRay();
    }

    #region Raycasts
    /// <summary>
    /// Casts a ray to provide the user with immediate feedback about what thing they are looking at.
    /// </summary>
    public void CastLookingRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactionDistance, _mask))
        {
            Debug.DrawLine(transform.position, _hit.point, Color.yellow);

            LookingAt = _hit.collider.GetComponent<Interactable>();
            return;
        }

        Debug.DrawLine(transform.position, transform.position + transform.forward * interactionDistance, Color.white);
        LookingAt = null;
    }

    /// <summary>
    /// Cast the interaction ray and interact with the first interactable that is hit.
    /// The logic is performed on the server
    /// </summary>
    private void CastInteractionRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactionDistance, _mask))
        {
            var interactable = _hit.collider.GetComponent<Interactable>();
            if (interactable != null) interactable.Interact(Player);

            Debug.DrawLine(transform.position, _hit.point, Color.green);
        }
    }
    #endregion
}
