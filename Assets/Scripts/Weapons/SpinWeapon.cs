using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private float rotationSpeed = 200f;
    
    [SerializeField] private Transform weaponToSpawn;
    [SerializeField] private float timeBetweenSpawns = 3f;
    [SerializeField] private float weaponLifetime = 2f;

    private float _spawnTimer;

    private void Update()
    {
       HandleRotation();
       HandleSpawns();
    }

    private void HandleRotation()
    {
        holder.rotation = Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + rotationSpeed * Time.deltaTime); 
    }

    private void HandleSpawns()
    {
        _spawnTimer -= Time.deltaTime;
        if (_spawnTimer <= 0)
        {
            Transform weapon = Instantiate(weaponToSpawn, weaponToSpawn.position, weaponToSpawn.rotation, holder);
            weapon.gameObject.SetActive(true);
            Destroy(weapon.gameObject, weaponLifetime);
            
            _spawnTimer = timeBetweenSpawns;
        }
    }
}
