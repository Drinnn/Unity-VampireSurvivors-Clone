using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyRarity
{
    COMMON,
    RARE,
    EPIC
}

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn config")] 
    [SerializeField] private EnemySpawnInfoListSO enemySpawnInfoListSO;
    [SerializeField] private RaritySpawnInfoListSO raritySpawnInfoListSO;

    [Header("Transform Config")]
    [SerializeField] private Transform minSpawnPointTransform;
    [SerializeField] private Transform maxSpawnPointTransform;
    
    [Header("Despawn Config")]
    [SerializeField] private float despawnBuffer = 4f;
    [SerializeField] private int enemiesAmountToCheckPerFrame = 10;

    private Transform _playerTransform;

    private Dictionary<EnemySpawnInfoSO, bool> _populateEnemySpawnInfoSODictionary;
    private Dictionary<EnemyRarity, List<GameObject>> _readyEnemiesSpawnInfoSOByRarityDictionary;

    private float _passedTime;

    private float _despawnDistance;
    private List<GameObject> _spawnedEnemiesList;
    private int _enemyToCheckIndex;

    private void Awake()
    {
        _populateEnemySpawnInfoSODictionary = new Dictionary<EnemySpawnInfoSO, bool>();
        foreach (var enemySpawnInfoSO in enemySpawnInfoListSO.list)
        {
            _populateEnemySpawnInfoSODictionary[enemySpawnInfoSO] = false;
        }

        _readyEnemiesSpawnInfoSOByRarityDictionary = new Dictionary<EnemyRarity, List<GameObject>>();
        foreach (var raritySpawnInfoSO in raritySpawnInfoListSO.list)
        {
            _readyEnemiesSpawnInfoSOByRarityDictionary[raritySpawnInfoSO.rarity] = new List<GameObject>();
        }
        
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
        
        _passedTime += Time.deltaTime;

        if (_populateEnemySpawnInfoSODictionary.Any(populatedEnemySpawnInfoSOKv => populatedEnemySpawnInfoSOKv.Value == false))
        {
            foreach (var populateEnemySpawnInfoSOKv in _populateEnemySpawnInfoSODictionary.Where(populatedEnemySpawnInfoSOKv => populatedEnemySpawnInfoSOKv.Value == false))
            {
                if (_passedTime >= populateEnemySpawnInfoSOKv.Key.spawnAfter)
                {
                    _readyEnemiesSpawnInfoSOByRarityDictionary[populateEnemySpawnInfoSOKv.Key.enemyRarity].Add(populateEnemySpawnInfoSOKv.Key.enemyPrefab);
                    _populateEnemySpawnInfoSODictionary[populateEnemySpawnInfoSOKv.Key] = true;
                }
            }
        }

        HandleSpawn();
        HandleEnemiesCleanup();
    }

    private void HandleSpawn()
    {
        // _commonSpawnTimer -= Time.deltaTime;
        // if (_commonSpawnTimer <= 0)
        // {
        //     _commonSpawnTimer = commonSpawnTime;
        //     GameObject enemy = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
        //     _spawnedEnemiesList.Add(enemy);
        // }
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
