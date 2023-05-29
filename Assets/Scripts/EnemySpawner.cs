using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 3f;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform minSpawnPointTransform;
    [SerializeField] private Transform maxSpawnPointTransform;

    private Transform _playerTransform;
    
    private float _spawnTimer;

    private void Awake()
    {
        _spawnTimer = timeToSpawn;
    }

    private void Start()
    {
        _playerTransform = PlayerController.Instance.transform;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
        
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            _spawnTimer = timeToSpawn;
            Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
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
