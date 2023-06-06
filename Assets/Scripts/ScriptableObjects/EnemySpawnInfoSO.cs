using UnityEngine;

[CreateAssetMenu]
public class EnemySpawnInfoSO : ScriptableObject
{
    public string enemyName;
    public EnemyRarity enemyRarity;
    public GameObject enemyPrefab;
    public float spawnAfter;
}
