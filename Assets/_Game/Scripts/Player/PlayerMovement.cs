using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputHandler _input;
    [SerializeField] private float _moveSpeed = 2;

    private Vector2 _moveDirection = Vector2.zero;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_input == null) return;

        if (_input.TouchHeld)
        {
            // calculate move direction
            _moveDirection = _input.TouchCurrentPosition 
                - _input.TouchStartPosition;
            _moveDirection.Normalize();
        }
    }

    private void FixedUpdate()
    {
        if (_rigidbody2D == null) return;

        if (_input.TouchHeld)
        {
            // move in current move direction
            Vector2 offsetPos 
                = _moveDirection * _moveSpeed * Time.deltaTime;
            _rigidbody2D.MovePosition
                (_rigidbody2D.position + offsetPos);
            //Debug.Log(_moveDirection);
        }
    }
}
