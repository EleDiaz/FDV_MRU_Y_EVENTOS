using System.Collections;
using System;
using UnityEngine;

public class LocomotionController : MonoBehaviour {
    private const float FallingThreshold = -0.1F;

    private CapsuleCollider _capsuleCollider;

    private bool _isFalling = false;

    private bool _isJumping = false;

    private bool _isSprinting = false;

    private Vector2 _movementDirection = Vector2.zero;

    private Rigidbody _rigidbody;

    [Tooltip("Maximum running speed units per seconds")]
    public float maxRunSpeed = 2.0f;

    [Tooltip("Maximum walking speed units per seconds")]
    public float maxWalkSpeed = 1.0f;

    [Tooltip("Current Speed(You shouldn't set up this)")]
    [SerializeField]
    public Vector2 speed = Vector2.zero;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        UpdateGrounding();
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        // Braking
        if (_movementDirection.magnitude == 0.0f && (!_isJumping || !_isFalling))
        {
            speed = Vector2.zero;
        }
        // We are Accelerating in some way by the user
        else if (!_isJumping && !_isFalling)
        {
            var auxMaxSpeed = _isSprinting ? maxRunSpeed : maxWalkSpeed;
            speed = _movementDirection * auxMaxSpeed * Time.deltaTime;
        }

        _rigidbody.velocity = speed;
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
            if (_isFalling)
            {
                _isFalling = false;
                _isJumping = false;
            }
        }
        else
        {
            if (_isJumping)
            {
                _isFalling = _rigidbody.velocity.y < FallingThreshold;
            }
            else
            {
                _isFalling = true;
            }
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
        // if (_currentGroundKind != GroundKind.NoGround)
        // {
        //     Debug.Log("Jumping=??");
        //     _rigidbody.AddForce(
        //         new Vector2(
        //             0,
        //             jumpForce * Mathf.Clamp(Mathf.Abs(Speed / maxWalkSpeed), 1, 2)), // TODO: WalkSpeed Problem
        //         ForceMode2D.Force);

        //     _isJumping = true;
        // }
    }

    public void MovementDirection(Vector2 value)
    {
        _movementDirection = value;
    }
}