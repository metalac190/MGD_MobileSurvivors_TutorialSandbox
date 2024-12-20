using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerCharacter _playerCharacter;

    [Header("Spawn Settings")]
    [SerializeField] private Enemy[] _possibleEnemiesToSpawn;
    [SerializeField] private float _spawnRate = 1;
    [SerializeField] private LayerMask _layersToTest;
    [SerializeField] private float _spawnDistanceFromPlayer = 10;
    [SerializeField] private float _timeBetweenSpawnRateChange = 10;
    [SerializeField] private float _spawnRateReductionAmount = 0.1f;
    [SerializeField] private float _minSpawnRate = .2f;

    private float _timeSinceLastSpawnRateChange = 0;
    private int _maxSpawnAttempts = 4;
    // spawn routine instance stored here
    private Coroutine _spawnRoutine;

    private void Start()
    {
        StartSpawning();
    }
    private void Update()
    {
        // increase our spawn cooldown time tracker
        _timeSinceLastSpawnRateChange += Time.deltaTime;
        // if our time since last reduction has met the reqs
        if(_timeSinceLastSpawnRateChange 
            >= _timeBetweenSpawnRateChange)
        {
            // reduce spawn cooldown rate
            _spawnRate -= _spawnRateReductionAmount;
            // make sure we don't go below our minimum
            if (_spawnRate < _minSpawnRate)
                _spawnRate = _minSpawnRate;
            // reset our seconds tracker to count up again
            _timeSinceLastSpawnRateChange = 0;
        }
    }
    // coroutines require an IEnumerator return value
    // and a return somewhere in the body
    private IEnumerator SpawnRoutine()
    {
        Vector2 spawnPoint;
        // infinite loop, but break out while waiting
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
            spawnPoint = GetValidWorldSpawnPoint();
            // if we have a valid spawn point, spawn!
            if(spawnPoint != Vector2.zero)
            {
                Spawn(ChooseRandomEnemy(), spawnPoint);
            }
        }
    }

    private Enemy ChooseRandomEnemy()
    {
        int randomEnemyIndex;
        // choose a random 'index' in the array (min/max)
        randomEnemyIndex = Random.Range
            (0, _possibleEnemiesToSpawn.Length);
        // return the Enemy prefab ref at that index in the aray
        return _possibleEnemiesToSpawn[randomEnemyIndex];
    }
    public void StopSpawning()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
    }
    public void StartSpawning()
    {
        if (_spawnRoutine != null)
            StopCoroutine(_spawnRoutine);
        _spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    public void Spawn(Enemy enemyToSpawn, Vector2 position)
    {
        // spawn enemy at the position and rotation
        Enemy newEnemy = Instantiate
            (enemyToSpawn, position, Quaternion.identity);
        newEnemy.Initialize(_playerCharacter);
    }

    public Vector2 GetValidWorldSpawnPoint()
    {
        // get random point on circle
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        // convert player position from Vector3 to Vector2
        Vector2 playerPos = new Vector2
            (_playerCharacter.transform.position.x,
            _playerCharacter.transform.position.y);
        // create point outwards from player pos in direction
        Vector2 testPoint = playerPos 
            + (randomDirection * _spawnDistanceFromPlayer);
        // test if there's a collider present on the test point
        for (int i = 0; i < _maxSpawnAttempts; i++)
        {
            // if no colliders present, return the position!
            // radius here is roughly the size of collider (.5)
            if (Physics2D.OverlapCircle
                (testPoint, .5f, _layersToTest) == null)
                return testPoint;
            // otherwise rotate 90* and try again
            else
                testPoint = new Vector2(testPoint.y, -testPoint.x);
        }
        Debug.Log("Could not spawn");
        // we've done our loops and testpoint is still default
        return testPoint = Vector2.zero;
    }
}
