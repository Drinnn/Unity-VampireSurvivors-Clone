using UnityEngine;

public class SpinWeapon : BaseWeapon
{
    private float _rotationSpeed;
    private GameObject _spawnable;
    private float _spawnableLifetime;
    private float _timeBetweenSpawnableSpawns;

    protected override void Setup()
    {
        var playerLevel = ExpSystem.Instance.CurrentLevel;
        
        _rotationSpeed = weaponObjectSO.levelStatsDictionary.GetValueByKey(playerLevel).speed;
        _spawnable = weaponObjectSO.spawnable;
        _spawnableLifetime = weaponObjectSO.levelStatsDictionary.GetValueByKey(playerLevel).duration;
        _timeBetweenSpawnableSpawns = weaponObjectSO.levelStatsDictionary.GetValueByKey(playerLevel).timeBetweenSpawns;

        isReady = true;
    }

    private void Update()
    {
        if (isReady)
        {
            HandleRotation();
            HandleSpawns();
        }
    }
    
    private void HandleRotation()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + _rotationSpeed * Time.deltaTime); 
    }
    
    private void HandleSpawns()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GameObject weapon = Instantiate(_spawnable, _spawnable.transform.position, Quaternion.identity, holder).gameObject;
            weapon.SetActive(true);
            weapon.GetComponent<IWeaponObject>().Setup(_spawnableLifetime);
            
            spawnTimer = _timeBetweenSpawnableSpawns;
        }
    }
}
