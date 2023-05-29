using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float timeToSpawn = 3f;
    [SerializeField] private GameObject enemyPrefab;

    private float _spawnTimer;

    private void Awake()
    {
        _spawnTimer = timeToSpawn;
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            _spawnTimer = timeToSpawn;
            Instantiate(enemyPrefab, transform.position, Quaternion.identity);
        }
    }
}
