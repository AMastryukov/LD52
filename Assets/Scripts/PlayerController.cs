using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public static Action OnOpenDatapad;
    public static Action OnCloseDatapad;
    public static Action OnStopUsingComputer;

    public State CurrentState => _currentState;

    public enum State { Movement, Sleep, Datapad, Computer, Dead, Frozen }

    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float crouchSpeed = 1f;

    private Transform _view;
    private PlayerInteractor _interactor;
    private Transform _camera;
    private CharacterController _controller;

    // Movement Inputs
    private Vector2 _lookInput;
    private Vector2 _moveInput;
    private bool _isCrouching;

    private State _currentState = State.Movement;

    private void Awake()
    {
        _view = GetComponentInChildren<PlayerInteractor>().transform;
        _interactor = _view.GetComponent<PlayerInteractor>();
        _controller = GetComponent<CharacterController>();
        _camera = _view.GetComponentInChildren<Camera>().transform;

        Player.OnDeath += Dead;
    }

    private void OnDestroy()
    {
        Player.OnDeath -= Dead;
    }

    private void Update()
    {
        GetInput();

        _interactor.enabled = _currentState != State.Computer;
        if (_currentState != State.Movement) return;

        Look();
        Move();
        Crouch();

        Interact();
    }

    public void LookAt(Transform transform)
    {
        _view.LookAt(transform);
    }

    private void GetInput()
    {
        _lookInput = new Vector2(-Input.GetAxisRaw("Mouse Y"), Input.GetAxisRaw("Mouse X"));
        _moveInput = new Vector2(Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal")).normalized;

        _isCrouching = Input.GetKey(KeyCode.LeftControl);

        // Datapad & Computer input
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (_currentState == State.Movement) TryOpenDatapad();
            else if (_currentState == State.Datapad) CloseDataPad();
            else if (_currentState == State.Computer) StopUsingComputer();
        }
    }

    private void Crouch()
    {
        // Change controller height according to whether the player is crouching or not
        if (_isCrouching) _controller.height = Mathf.Lerp(_controller.height, 1f, 0.05f);
        else _controller.height = Mathf.Lerp(_controller.height, 2f, 0.05f);
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

        var moveSpeed = _isCrouching ? crouchSpeed : walkSpeed;
        _controller.Move(moveSpeed * Time.deltaTime * movement);
    }

    private void Interact()
    {
        // Interaction input
        _interactor.CastLookingRay();
        if (Input.GetKeyDown(KeyCode.E)) _interactor.CastInteractionRay();
    }

    #region Control State Management
    public bool TryUseComputer(Transform viewPosition)
    {
        if (_currentState != State.Movement) return false;
        _currentState = State.Computer;
        Cursor.lockState = CursorLockMode.Confined;

        // Move camera to new position & rotation
        _camera.position = viewPosition.position;
        _camera.rotation = viewPosition.rotation;

        return true;
    }

    public void StopUsingComputer()
    {
        if (_currentState != State.Computer) return;
        _currentState = State.Movement;
        Cursor.lockState = CursorLockMode.Locked;

        // Reset camera position & rotation
        _camera.localPosition = Vector3.zero;
        _camera.localRotation = Quaternion.identity;

        OnStopUsingComputer?.Invoke();
    }

    public bool TryOpenDatapad()
    {
        if (_currentState != State.Movement) return false;
        _currentState = State.Datapad;
        Cursor.lockState = CursorLockMode.Confined;

        OnOpenDatapad?.Invoke();

        return true;
    }

    public void CloseDataPad()
    {
        if (_currentState != State.Datapad) return;
        _currentState = State.Movement;
        Cursor.lockState = CursorLockMode.Locked;

        OnCloseDatapad?.Invoke();
    }

    public bool TrySleep()
    {
        if (_currentState != State.Movement) return false;
        _currentState = State.Sleep;
        Cursor.lockState = CursorLockMode.Locked;

        return true;
    }

    public void WakeUp()
    {
        if (_currentState != State.Sleep) return;
        _currentState = State.Movement;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Dead(string cause)
    {
        _currentState = State.Dead;
    }
    #endregion

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
