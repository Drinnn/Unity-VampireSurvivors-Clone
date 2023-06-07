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
    private Dictionary<EnemyRarity, float> _raritiesTimerDictionary;

    private float _passedTime;

    private float _despawnDistance;
    private List<GameObject> _spawnedEnemiesList;
    private int _enemyToCheckIndex;

    private void Awake()
    {
        _spawnedEnemiesList = new List<GameObject>();
    }

    private void Start()
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

        _raritiesTimerDictionary = new Dictionary<EnemyRarity, float>();
        foreach (var raritySpawnInfoSO in raritySpawnInfoListSO.list)
        {
            _raritiesTimerDictionary[raritySpawnInfoSO.rarity] = raritySpawnInfoSO.spawnTime;
        }
        
        _playerTransform = PlayerController.Instance.transform;

        _despawnDistance = Vector3.Distance(transform.position, maxSpawnPointTransform.position) + despawnBuffer;
    }

    private void Update()
    {
        transform.position = _playerTransform.position;
        
        _passedTime += Time.deltaTime;

        HandleTimers();
        HandleEnemiesPopulate();
        HandleEnemiesCleanup();
    }

    private void HandleTimers()
    {
        foreach (var rarity in _raritiesTimerDictionary.Keys.ToList())
        {
            _raritiesTimerDictionary[rarity] -= Time.deltaTime;
            if (_raritiesTimerDictionary[rarity] <= 0)
            {
             var raritySpawnInfo =
                        raritySpawnInfoListSO.list.First(raritySpawnInfoSO => raritySpawnInfoSO.rarity == rarity);
             _raritiesTimerDictionary[rarity] = raritySpawnInfo.spawnTime;
                HandleSpawn(rarity);
            }
        }
    }

    private void HandleSpawn(EnemyRarity rarity)
    {
       
        var rarityReadyEnemiesKv = _readyEnemiesSpawnInfoSOByRarityDictionary.First(readyEnemySpawnInfoSOKv => readyEnemySpawnInfoSOKv.Key == rarity);
        if (rarityReadyEnemiesKv.Value.Count > 0)
        {
            var enemyToSpawn = rarityReadyEnemiesKv.Value[Random.Range(0, rarityReadyEnemiesKv.Value.Count)];
            GameObject enemy = Instantiate(enemyToSpawn, GetRandomSpawnPoint(), Quaternion.identity);
            _spawnedEnemiesList.Add(enemy);  
        }
    }

    private void HandleEnemiesPopulate()
    {
        if (_populateEnemySpawnInfoSODictionary.Any(populatedEnemySpawnInfoSOKv => populatedEnemySpawnInfoSOKv.Value == false))
        {
            foreach (var populateEnemySpawnInfoSOKv in _populateEnemySpawnInfoSODictionary.Where(populatedEnemySpawnInfoSOKv => populatedEnemySpawnInfoSOKv.Value == false).ToList())
            {
                if (_passedTime >= populateEnemySpawnInfoSOKv.Key.spawnAfter)
                {
                    _readyEnemiesSpawnInfoSOByRarityDictionary[populateEnemySpawnInfoSOKv.Key.enemyRarity].Add(populateEnemySpawnInfoSOKv.Key.enemyPrefab);
                    _populateEnemySpawnInfoSODictionary[populateEnemySpawnInfoSOKv.Key] = true;
                }
            }
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
