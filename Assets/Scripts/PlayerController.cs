using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance { get; private set; }

    public event EventHandler OnTookDamage;

    public float CurrentHealth => _currentHealth;
    
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 2.8f;

    public bool IsMoving => _isMoving;

    private float _currentHealth;
    private bool _isMoving;

    private void Awake()
    {
        Instance = this;
        
        _currentHealth = maxHealth;
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 playerInput = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(playerInput.x, playerInput.y, 0);

        transform.position += movementDirection * moveSpeed * Time.deltaTime;

        _isMoving = movementDirection != Vector3.zero;
    }

    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        
        OnTookDamage?.Invoke(this, EventArgs.Empty);
        
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            // Handle game over...
        }
    }

    public void TakeDamageWithKnockBack(float amount, float knockBackTime, float knockBackMultiplier)
    {
        TakeDamage(amount);
    }
}
