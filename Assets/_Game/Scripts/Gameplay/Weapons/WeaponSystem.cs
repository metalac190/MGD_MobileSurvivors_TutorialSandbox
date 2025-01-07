using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public string Name { get; private set; }
    // general
    int _damage = 5;
    float _moveSpeed = 5;
    float _lifetime = 2;
    float _cooldown = 1;
    private Projectile _projectile = null;
    // detection
    private bool _onlyFireIfNearbyTargets = false;
    private float _detectionRadius = 10;
    private ContactFilter2D _targetFilter;

    private List<Collider2D> _targetsDetected = new List<Collider2D>();
    private Vector2 _direction = Vector2.zero;
    public void SetupWeapon(WeaponData data)
    {
        // assign the data into local variable
        Name = data.Name;
        _damage = data.Damage;
        _moveSpeed = data.MoveSpeed;
        _lifetime = data.Lifetime;
        _cooldown = data.Cooldown;
        _projectile = data.Projectile;
        _onlyFireIfNearbyTargets = data.OnlyFireIfNearbyTargets;
        _detectionRadius = data.DetectionRadius;
        _targetFilter = data.TargetFilter;
    }
    public void IncreaseDamage(int increaseAmount)
    {
        _damage += increaseAmount;
    }

    private void Start()
    {
        // (MethodName, delay, repeatRate)
        InvokeRepeating(nameof(Attack), _cooldown, _cooldown);
    }

    public void Attack()
    {
        // only check for targets if we're supposed to hold fire until near
        if (_onlyFireIfNearbyTargets)
        {
            Collider2D target = DetectClosestTarget();
            // if there's no target, don't fire
            if (target == null) return;

            _direction = target.transform.position - transform.position;
            // get direction scaled to 1 value
            _direction.Normalize();
        }

        Projectile newProjectile = Instantiate
            (_projectile, transform.position, Quaternion.identity);
        newProjectile.Spawn(_direction, _damage, _moveSpeed);
        // remove our newProjectile gameObject after X seconds
        Destroy(newProjectile.gameObject, _lifetime);
    }

    private Collider2D DetectClosestTarget()
    {
        // detect
        Physics2D.OverlapCircle(transform.position, _detectionRadius,
            _targetFilter, _targetsDetected);
        // if no enemies detected, return null
        if (_targetsDetected == null) return null;
        // test for closest enemy
        // use these variables to store closest as we test
        Collider2D closestTarget = null;
        float closestDistance = Mathf.Infinity;
        // test!
        foreach (Collider2D target in _targetsDetected)
        {
            // draw the line between projectile and enemy
            Vector3 distanceVector = target.transform.position - transform.position;
            // get length of line, as a number value
            float currentDistance = distanceVector.sqrMagnitude;
            // if it's closer than our closest, store distance and target
            if (currentDistance <= closestDistance)
            {
                closestTarget = target;
                closestDistance = currentDistance;
            }
        }
        // return our best results
        return closestTarget;
    }

    public void DecreaseCooldown(float amount)
    {
        _cooldown -= amount;
        // don't allow cooldown to go below 0 (that would be wild)
        if (_cooldown < 0.1f)
            _cooldown = 0.1f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
