using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [Header("General")]
    [SerializeField] int _damage = 5;
    [SerializeField] float _moveSpeed = 5;
    [SerializeField] float _lifetime = 2;
    [SerializeField] float _cooldown = 1;
    [SerializeField] Projectile _projectile = null;

    [Header("Detection")]
    [SerializeField] private bool _onlyFireIfNearbyTargets = false;
    [Range(1, 20)]
    [SerializeField] private float _detectionRadius = 10;
    [SerializeField] private ContactFilter2D _targetFilter;

    private List<Collider2D> _targetsDetected = new List<Collider2D>();
    private Vector2 _direction = Vector2.zero;

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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _detectionRadius);
    }
}
