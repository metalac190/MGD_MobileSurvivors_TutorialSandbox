using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1;

    private PlayerCharacter _player;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
    // Send player ref through initialization to avoid scene search
    public void Initialize(PlayerCharacter player)
    {
        _player = player;
    }
    private void FixedUpdate()
    {
        MoveTowardsPlayer();
    }
    private void MoveTowardsPlayer()
    {
        if (_player == null) return;
        // get direction from player
        Vector2 direction = _player.transform.position - transform.position;
        direction.Normalize();
        // calculate target position offset
        Vector2 offsetPos = direction * _moveSpeed * Time.deltaTime;
        // add offset to current (local)
        _rigidbody2D.MovePosition(_rigidbody2D.position + offsetPos);
    }
}
