using System;
using System.Collections.Generic;
using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public static ExpSystem Instance { get; private set; }

    public int CurrentLevel => _currentLevel;
    
    public event EventHandler OnExpUpdate;

    [SerializeField] private Transform expPickupPrefab;
    [SerializeField] private ExpLevelConfigSO expLevelConfigSO;

    private List<float> _expLevels; 
        
    private float _currentXp;
    private int _currentLevel;
    
    private void Awake()
    {
        Instance = this;

        _expLevels = new List<float>();
        
        _currentXp = 0;
        _currentLevel = 1;
        
        for (int i = 0; i < expLevelConfigSO.levelAmount; i++)
        {
            if (i == 0)
            {
                _expLevels.Add(expLevelConfigSO.initialLevelExp);
            }
            else
            {
                _expLevels.Add(_expLevels[i - 1] * expLevelConfigSO.levelMultiplier);
            }
        }
    }

    public void AddXp(float amount)
    {
        _currentXp += amount;
        if (_currentXp >= _expLevels[_currentLevel - 1])
        {
            LevelUp();
        }
        
        OnExpUpdate?.Invoke(this, EventArgs.Empty);
    }

    private void LevelUp()
    {
        if (_currentLevel == _expLevels.Count)
        {
            return;
        }
        
        _currentXp -= _expLevels[_currentLevel - 1];
        _currentLevel++;
    }

    public void SpawnExpPickup(Vector3 position, float bonusAmount)
    {
        GameObject expPickup = Instantiate(expPickupPrefab, position, Quaternion.identity).gameObject;
        expPickup.GetComponent<ExpPickup>().AddExpBonus(bonusAmount);
    }

    public float GetExpNormalized()
    {
        if (_currentLevel == _expLevels.Count)
        {
            return 1f;
        }
        
        float remainingExpToNextLevel = _expLevels[_currentLevel - 1] - _currentXp;
        
        return 1f - remainingExpToNextLevel / _expLevels[_currentLevel - 1];
    }
}
