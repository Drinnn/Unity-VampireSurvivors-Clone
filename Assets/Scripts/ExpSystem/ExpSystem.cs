using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public static ExpSystem Instance { get; private set; }

    [SerializeField] private Transform expPickupPrefab;

    private float _currentXp;
    
    private void Awake()
    {
        Instance = this;
    }

    public void AddXp(float amount)
    {
        _currentXp += amount;
    }

    public void SpawnExpPickup(Vector3 position, float bonusAmount)
    {
        GameObject expPickup = Instantiate(expPickupPrefab, position, Quaternion.identity).gameObject;
        expPickup.GetComponent<ExpPickup>().AddExpBonus(bonusAmount);
    }
}
