using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _currentHealth = 100;
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
        Destroy(gameObject);
    }
}
