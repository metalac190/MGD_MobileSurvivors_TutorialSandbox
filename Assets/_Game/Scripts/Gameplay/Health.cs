using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 100;
    [SerializeField] private int _healthMax = 100;

    public UnityEvent OnKilled;
    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        // make sure we stay within valid health range (min, max)
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _healthMax);
        Debug.Log("New Health: " + _currentHealth);
    }
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
