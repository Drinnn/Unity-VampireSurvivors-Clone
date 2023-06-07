using UnityEngine;

public class ExpSystem : MonoBehaviour
{
    public static ExpSystem Instance { get; private set; }

    private float _currentXp;
    
    private void Awake()
    {
        Instance = this;
    }

    public void AddXp(float amount)
    {
        _currentXp += amount;
    }
}
