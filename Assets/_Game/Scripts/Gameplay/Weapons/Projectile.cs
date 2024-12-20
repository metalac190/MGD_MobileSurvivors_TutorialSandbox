using NUnit.Framework;
using UnityEngine;
using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private int _damage = 5;
    private float _moveSpeed = 5;
    private Vector2 _direction = Vector2.zero;

    public void Spawn(Vector2 direction, int damage, float moveSpeed)
    {
        _direction = direction.normalized;
        _damage = damage;
        _moveSpeed = moveSpeed;
    }

    public void FixedUpdate()
    {
        // calculate target position offset
        Vector2 offsetPos = _direction * _moveSpeed * Time.deltaTime;
        // add offset to current (local)
        _rb.MovePosition(_rb.position + offsetPos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // search for Health component, apply damage
        // hit flash, sounds, etc.
        if (collision.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }


}
