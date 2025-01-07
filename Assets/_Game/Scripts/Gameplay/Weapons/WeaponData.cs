using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData_", 
    menuName = "ScriptableObjects/WeaponData")]
public class WeaponData : ScriptableObject
{
    [Header("General")]
    [SerializeField] string _name = "Default";
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
    // => is shorthand for return value, but don't let them set
    public string Name => _name;
    public int Damage => _damage;
    public float MoveSpeed => _moveSpeed;
    public float Lifetime => _lifetime;
    public float Cooldown => _cooldown;
    public Projectile Projectile => _projectile;
    public bool OnlyFireIfNearbyTargets => _onlyFireIfNearbyTargets;
    public float DetectionRadius => _detectionRadius;
    public ContactFilter2D TargetFilter => _targetFilter;
}
