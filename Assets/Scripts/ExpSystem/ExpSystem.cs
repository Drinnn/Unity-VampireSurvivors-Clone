using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public static ExpSystem Instance { get; private set; }

    [SerializeField] private Transform expPickupPrefab;
    [SerializeField] private ExpLevelConfigSO expLevelConfigSO;

    private List<int> _expLevels; 
        
    private float _currentXp;
    private int _currentLevel;
    
    private void Awake()
    {
        Instance = this;

        _expLevels = new List<int>();
        for (int i = 0; i <= expLevelConfigSO.levelAmount; i++)
        {
            if (i == 0)
            {
                _expLevels.Add((int)expLevelConfigSO.initialLevelExp);
            }
            else
            {
                _expLevels.Add(Mathf.CeilToInt(_expLevels[i - 1] * expLevelConfigSO.levelMultiplier));
            }
        }

        _currentXp = 0;
        _currentLevel = 1;
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
