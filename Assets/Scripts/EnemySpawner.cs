using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 3f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform minSpawnPointTransform;
    [SerializeField] private Transform maxSpawnPointTransform;
    [SerializeField] private float despawnBuffer = 4f;
    [SerializeField] private int enemiesAmountToCheckPerFrame = 10;

    private Transform _playerTransform;
    
    private float _spawnTimer;
    private float _despawnDistance;
    private List<GameObject> _spawnedEnemiesList;
    private int _enemyToCheckIndex;

    private void Awake()
    {
        _spawnTimer = timeToSpawn;
        _spawnedEnemiesList = new List<GameObject>();
    }

    private void Start()
    {
        _playerTransform = PlayerController.Instance.transform;

        _despawnDistance = Vector3.Distance(transform.position, maxSpawnPointTransform.position) + despawnBuffer;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
        
        HandleSpawn();
        HandleEnemiesCleanup();
    }

    private void HandleSpawn()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            _spawnTimer = timeToSpawn;
            GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            _spawnedEnemiesList.Add(enemy);
        }
    }

    private void HandleEnemiesCleanup()
    {
        int checkTargetIndex = _enemyToCheckIndex + enemiesAmountToCheckPerFrame;
        while (_enemyToCheckIndex < checkTargetIndex)
        {
            if (_enemyToCheckIndex < _spawnedEnemiesList.Count)
            {
                if (_spawnedEnemiesList[_enemyToCheckIndex] != null)
                {
                    if (Vector3.Distance(transform.position,
                            _spawnedEnemiesList[_enemyToCheckIndex].transform.position) > _despawnDistance)
                    {
                        Destroy(_spawnedEnemiesList[_enemyToCheckIndex]);
                        
                        _spawnedEnemiesList.RemoveAt(_enemyToCheckIndex);
                        checkTargetIndex--;
                    }
                    else
                    {
                        _enemyToCheckIndex++;
                    }
                }
                else
                {
                    _spawnedEnemiesList.RemoveAt(_enemyToCheckIndex);
                    checkTargetIndex--;
                }
            }
            else
            {
                _enemyToCheckIndex = 0;
                checkTargetIndex = 0;
            }
        }
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 spawnPoint = Vector3.zero;

        bool spawnVerticalEdge = Random.Range(0f, 1f) > .5f;
        if (spawnVerticalEdge)
        {
            spawnPoint.y = Random.Range(minSpawnPointTransform.position.y, maxSpawnPointTransform.position.y);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.x = maxSpawnPointTransform.position.x;
            }
            else
            {
                spawnPoint.x = minSpawnPointTransform.position.x;
            }
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawnPointTransform.position.x, maxSpawnPointTransform.position.x);
            if (Random.Range(0f, 1f) > .5f)
            {
                spawnPoint.y = maxSpawnPointTransform.position.y;
            }
            else
            {
                spawnPoint.y = minSpawnPointTransform.position.y;
            }
        }
        
        return spawnPoint;
    }
}
