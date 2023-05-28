using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRadius = 0.8f;

    private Transform _target;
    private bool _isTargetLocked;
    private float _currentAttackTimer;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerController>().transform;
        _currentAttackTimer = 0;
    }

    private void Update()
    {
        HandlePlayerLock();
        HandlePlayerAttack();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_isTargetLocked)
        {
            Vector3 movementDirection = (_target.position - transform.position).normalized;

            transform.position += movementDirection * moveSpeed * Time.deltaTime;
        }
    }

    private void HandlePlayerLock()
    {
        if (Vector3.Distance(transform.position, _target.position) <= attackRadius)
        {
            _isTargetLocked = true;
        }
        else
        {
            _isTargetLocked = false;
        }
    }

    private void HandlePlayerAttack()
    {
        if (_isTargetLocked)
        {
            if (_currentAttackTimer <= 0)
            {
                PlayerController.Instance.TakeDamage(attackDamage);
                _currentAttackTimer = attackSpeed;
            }

            _currentAttackTimer -= Time.deltaTime;
        }
        else
        {
            _currentAttackTimer = 0f;
        }
    }
}