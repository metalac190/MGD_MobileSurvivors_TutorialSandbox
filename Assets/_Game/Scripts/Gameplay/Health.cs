using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 100;

    public UnityEvent OnKilled;
    public void TakeDamage(int amount)
    {
        _currentHealth -= amount;
        // check if dead
        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Kill();
        }
    }
    public void Kill()
    {
        OnKilled?.Invoke();

        Destroy(gameObject);
    }
}
