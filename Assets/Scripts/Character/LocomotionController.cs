using System.Collections;
using System;
using UnityEngine;

public class LocomotionController : MonoBehaviour {    /// <summary>
    /// <summary>
    /// Max walk speed
    /// </summary>
    public float maxWalkSpeed = 1.0f;

    /// <summary>
    /// Max run speed
    /// </summary>
    public float maxRunSpeed = 2.0f;

    /// <summary>
    /// Acceleration to reach a speed, it is used on the blend animations
    /// </summary>
    public float acceleration = 0.5f;

    /// <summary>
    ///
    /// </summary>
    public float rotationSpeed = 1.0f;

    /// <summary>
    /// Air control on jumps or falling, higher values let give you more control.
    /// </summary>
    [Range(0.0f, 1.0f)] public float airControl = 0.3f;

    /// <summary>
    /// Time to update the camera after some activity on the camera angle
    /// </summary>
    public float timeToResetCamera = 4.0f;

    public Camera playerCamera;

    private bool _isInAir = false;
    private bool _isSprinting = false;
    private Vector2 _lerpMovementDirection = Vector2.zero;
    private Vector2 _movementDirection = Vector2.zero;
    private Vector2 _movementDirectionBeforeJump = Vector2.zero;
    private Quaternion _lastHeadOrientation = Quaternion.identity;
    private Rigidbody _rigidbody;
    private CameraController _cameraController;
    private float _lastCameraCheckTime = 0.0f;
    private CapsuleCollider _capsuleCollider;

    private bool _lockFullSpeed = false;

    private double TOLERANCE = 0.1;
    public int jumpForce = 300;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _cameraController = GetComponent<CameraController>();
    }

    private void Start()
    {
    }

    public void ToogleLockFullSpeed() {
        Debug.Log("Toogle?");
        _lockFullSpeed = !_lockFullSpeed;
    }

    private void FixedUpdate()
    {
        UpdateGrounding();
        //UpdatePosition();

        var auxMaxSpeed = _isSprinting ? maxRunSpeed : maxWalkSpeed;
        if (_lockFullSpeed) {
            auxMaxSpeed = maxRunSpeed;
        }
        var new_pos = _movementDirection.normalized * auxMaxSpeed * Time.deltaTime;

        if (!_isInAir)
        {
            Move(new_pos);
        }
        else if (_isInAir)
        {
            Move(new_pos * airControl + _movementDirectionBeforeJump);
        }
    }

    private void Move(Vector2 movementDirection)
    {
        var moveDirection = transform.rotation * new Vector3(movementDirection.x, 0.0f, movementDirection.y);
        _rigidbody.MovePosition(transform.position + moveDirection);

        // Automatic rotation after X seconds
        if ((Math.Abs(_movementDirection.magnitude) > TOLERANCE || Time.time - _lastCameraCheckTime > timeToResetCamera) &&
            !_isInAir)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation.normalized, _cameraController.yawRotation,
                rotationSpeed * Time.deltaTime);

            if (Math.Abs(Quaternion.Angle(transform.rotation, _cameraController.yawRotation)) < TOLERANCE)
            {
                _lastCameraCheckTime = Time.time;
            }
        }
    }

    private void UpdatePosition()
    {
        Vector3 movement = Vector3.zero;
        // Braking
        if (_movementDirection.magnitude == 0.0f && !_isInAir)
        {
            return;
        }
        // We are Accelerating in some way by the user
        else if (!_isInAir)
        {
            var auxMaxSpeed = _isSprinting ? maxRunSpeed : maxWalkSpeed;
            var new_pos = _movementDirection.normalized * auxMaxSpeed * Time.deltaTime;

            _rigidbody.MovePosition(transform.position + new Vector3(new_pos.x, 0.0f, new_pos.y));
        }
    }

    bool CheckGrounded()
    {
        return Physics.CheckCapsule(_capsuleCollider.bounds.center,
            new Vector3(_capsuleCollider.bounds.center.x, _capsuleCollider.bounds.min.y - 0.1f,
                _capsuleCollider.bounds.center.z), _capsuleCollider.radius, ~LayerMask.GetMask("Player"));
    }

    /* TODO: we need to rewrite this, we could keep some of the logic due to different kind of terrain and their triggers*/
    void UpdateGrounding()
    {
        if (CheckGrounded())
        {
            _isInAir = false;
        }
    }

    public void SetWalking()
    {
        _isSprinting = false;
    }

    public void SetSprinting()
    {
        _isSprinting = true;
    }

    public void Jump()
    {
        if (!_isInAir)
        {
            _movementDirectionBeforeJump = _movementDirection;
            _rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    public void MovementDirection(Vector2 value)
    {
        _movementDirection = value;
    }
}