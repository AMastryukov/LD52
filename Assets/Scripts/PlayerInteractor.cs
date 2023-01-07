using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    public Interactable LookingAt { get; private set; }

    [Header("Settings")]
    [SerializeField] private float interactionDistance;

    private LayerMask _mask;
    private RaycastHit _hit;

    private void Awake()
    {
        _mask = Physics.AllLayers;
    }

    private void Update()
    {
        CastLookingRay();

        if (Input.GetKeyDown(KeyCode.E))
        {
            CastInteractionRay();
        }
    }

    #region Raycasts
    /// <summary>
    /// Casts a ray to provide the user with immediate feedback about what thing they are looking at.
    /// </summary>
    public void CastLookingRay()
    {
        if (Physics.Raycast(transform.position, transform.forward, out _hit, interactionDistance, _mask))
        {
            Debug.DrawLine(transform.position, _hit.point, Color.white);

            LookingAt = _hit.collider.GetComponent<Interactable>();
            return;
        }

        Debug.DrawLine(transform.position, transform.position + transform.forward * interactionDistance, Color.yellow);
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
            if (interactable != null) interactable.Interact(this);

            Debug.DrawLine(transform.position, _hit.point, Color.green);
        }
    }
    #endregion
}
