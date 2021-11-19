using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //TODO
    //1-Doble salto
    //2-Dash
    //3-Escalar
    //4-Stamina

    //----------------------------------------
    [Header("Components")] private Rigidbody2D _rb;
    public Animator _anim;

    [Header("Layer Masks")] [SerializeField]
    private LayerMask _groundLayer;

    [Header("Movement Variables")] [SerializeField]
    private float _movementAcceleration = 70f;

    [SerializeField] private float _maxMoveSpeed = 12f;
    [SerializeField] private float _groundLinearDrag = 7f;
    public float _horizontalDirection;

    private bool _changingDirection =>
        (_rb.velocity.x > 0f && _horizontalDirection < 0f) || (_rb.velocity.x < 0f && _horizontalDirection > 0f);

    [Header("Jump Variables")] [SerializeField]
    private float _jumpForce = 12f;

    [SerializeField] private float _airLinearDrag = 2.5f;
    [SerializeField] private float _fallMultiplier = 8f;
    [SerializeField] private float _lowJumpFallMultiplier = 5f;
    [SerializeField] private int _extraJumps = 1;
    [SerializeField] private float _coyoteTime = .1f;
    private float _coyoteCounter;
    private int _extraJumpsValue;
    private bool _canJump => Input.GetKeyDown(KeyCode.Space) && (_coyoteCounter > 0 || _extraJumpsValue > 0);

    [Header("Ground Collision Variables")] [SerializeField]
    private float _groundRaycastLength;

    [SerializeField] private Vector3 _groundRaycastOffset;
    private bool _onGround;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    private void Update()
    {
        _horizontalDirection = GetInput().x;

        Turn();

        if (_canJump) Jump();
    }

    private void FixedUpdate()
    {
        CheckCollisions();
        MoveCharacter();
        if (_onGround)
        {
            ApplyGroundLinearDrag();
            _extraJumpsValue = _extraJumps;
            _coyoteCounter = _coyoteTime;
        }
        else
        {
            ApplyAirLinearDrag();
            FallMultiplier();
            _coyoteCounter -= Time.deltaTime;
        }
    }

    private Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void MoveCharacter()
    {
        _rb.AddForce(new Vector2(_horizontalDirection, 0f) * _movementAcceleration);

        if (Mathf.Abs(_rb.velocity.x) > _maxMoveSpeed)
            _rb.velocity = new Vector2(Mathf.Sign(_rb.velocity.x) * _maxMoveSpeed, _rb.velocity.y);
    }

    private void Turn()
    {
        if (_horizontalDirection != Vector2.zero.x)
        {
            _anim.SetBool("Walking", true);

            if (_horizontalDirection > 0)
                transform.localScale = Vector3.one;

            else
                transform.localScale = new Vector3(-1, 1, 1);
        }

        else
            _anim.SetBool("Walking", false);
    }

    private void ApplyGroundLinearDrag()
    {
        if (Mathf.Abs(_horizontalDirection) < 0.4f || _changingDirection)
        {
            _rb.drag = _groundLinearDrag;
        }
        else
        {
            _rb.drag = 0f;
        }
    }

    private void ApplyAirLinearDrag()
    {
        _rb.drag = _airLinearDrag;
    }

    private void Jump()
    {
        if (!_onGround) _extraJumpsValue--;

        _rb.velocity = new Vector2(_rb.velocity.x, 0f);
        _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _coyoteCounter = 0f;
    }

    private void FallMultiplier()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.gravityScale = _fallMultiplier;
        }
        else if (_rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _rb.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            _rb.gravityScale = 1f;
        }
    }

    private void CheckCollisions()
    {
        _onGround =
            Physics2D.Raycast(transform.position + _groundRaycastOffset, Vector2.down, _groundRaycastLength,
                _groundLayer) || Physics2D.Raycast(transform.position - _groundRaycastOffset, Vector2.down,
                _groundRaycastLength, _groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + _groundRaycastOffset,
            transform.position + _groundRaycastOffset + Vector3.down * _groundRaycastLength);
        Gizmos.DrawLine(transform.position - _groundRaycastOffset,
            transform.position - _groundRaycastOffset + Vector3.down * _groundRaycastLength);
    }
}