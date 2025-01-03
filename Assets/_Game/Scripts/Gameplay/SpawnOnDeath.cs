using UnityEngine;

public class SpawnOnDeath : MonoBehaviour
{
    [SerializeField] private GameObject _prefabToSpawn;
    [SerializeField] private Health _healthToWatch;
    private void OnEnable()
    {
        // if we have a health component, add Spawn as callback
        // to the OnKilled event
        if (_healthToWatch != null)
            _healthToWatch.OnKilled?.AddListener(Spawn);
    }
    private void OnDisable()
    {
        // remove Callback from OnKilled event
        if (_healthToWatch != null)
            _healthToWatch.OnKilled?.RemoveListener(Spawn);
    }
    public void Spawn()
    {
        if(_prefabToSpawn != null)
        {
            Instantiate(_prefabToSpawn,
                transform.position, Quaternion.identity);
        }
    }
}