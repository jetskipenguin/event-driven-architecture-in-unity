using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _moveSpeed = 5f;

    [Header("Asset References")]
    [SerializeField] private InputReader _inputReader = default;

    private Vector2 _inputVector;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _inputReader.EnableGameplayInput();
    }

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _inputVector * Time.fixedDeltaTime * _moveSpeed);
    }

    // --- Event Listeners ---
    private void OnMove(Vector2 movementInput)
    {
        _inputVector = movementInput;
    }
}
