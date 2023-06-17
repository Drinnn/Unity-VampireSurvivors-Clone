using System.Linq;
using UnityEngine;

public class OrbittingFireball : MonoBehaviour
{
    [SerializeField] private WeaponSO weaponSO;

    private float _spawnTimer;

    private void Update()
    {
        HandleSpawnables();
    }

    private void HandleSpawnables()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            GameObject weaponObject = Instantiate(weaponSO.spawnable, transform.position, Quaternion.identity, transform).gameObject;
            weaponObject.SetActive(true);
            var stats = GetCurrentLevelStats();
            weaponObject.GetComponent<IWeaponObject>().Setup(stats.speed, stats.damage, stats.range, stats.duration);
            
            _spawnTimer = stats.timeBetweenSpawns;
        }
    }

    private WeaponStats GetCurrentLevelStats()
    {
        var currentWeaponLevel = weaponSO.levelStatsDictionary.keys.Last(key => key <= ExpSystem.Instance.CurrentLevel);

        return weaponSO.levelStatsDictionary.GetValueByKey(currentWeaponLevel);
    }
}
