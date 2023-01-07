using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 3f;

    private Transform _view;
    private CharacterController _controller;

    private Vector2 _lookInput;
    private Vector2 _moveInput;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        _view = GetComponentInChildren<Camera>().transform;
        _controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GetInput();
        Look();
        Move();
    }

    private void GetInput()
    {
        _lookInput = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
        _moveInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized;
    }

    private void Look()
    {
        // Calculate desired pitch & yaw, clamping the pitch
        var desiredViewPitch = ClampPitch(_view.rotation.eulerAngles.x + _lookInput.x);
        var desiredViewYaw = _view.rotation.eulerAngles.y + _lookInput.y;

        transform.rotation = Quaternion.Euler(0f, desiredViewYaw, 0f);
        _view.rotation = Quaternion.Euler(desiredViewPitch, desiredViewYaw, 0f);
    }

    private void Move()
    {
        // Convert movement input into vector
        var movement = transform.forward * _moveInput.x + transform.right * _moveInput.y;

        // Apply gravity
        movement.y = _controller.isGrounded ? -5f : movement.y - 5f;

        _controller.Move(maxSpeed * Time.deltaTime * movement);
    }

    #region Pitch Clamper
    private static float minPitch = 85f;
    private static float maxPitch = 85f;

    private static float ClampPitch(float pitch)
    {
        if (pitch < 360 - maxPitch && pitch > 180) return 360 - maxPitch;
        if (pitch > minPitch && pitch < 180) return minPitch;
        return pitch;
    }
    #endregion
}
