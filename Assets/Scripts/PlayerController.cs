using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static PlayerController Instance { get; private set; }
    
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
        if (_currentHealth <= 0)
        {
            Destroy(gameObject);
            // Handle game over...
        }
    }
    
    
}
