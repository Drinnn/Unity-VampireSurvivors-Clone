using System;
using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamageable
{
    public event EventHandler<OnTookDamageEventArgs> OnTookDamage;
    public class OnTookDamageEventArgs : EventArgs
    {
        public bool IsFatal;
    }

    [SerializeField] private EnemyAnimator animator;
    
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float moveSpeed = 1.5f;

    [SerializeField] private float attackSpeed = 1f;
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRadius = 0.8f;

    private float _currentHealth;
    private float _currentMoveSpeed;
    private Transform _target;
    private bool _isTargetLocked;
    private float _currentAttackTimer;
    private float _knockBackTime;
    private float _knockBackTimer;
    private float _knockBackMultiplier;

    private void Awake()
    {
        _currentHealth = maxHealth;
        _currentMoveSpeed = moveSpeed;
        _target = FindObjectOfType<PlayerController>().transform;
        _currentAttackTimer = 0;
    }

    private void Update()
    {
        HandlePlayerLock();
        HandlePlayerAttack();
        HandleMovement();
        HandleKnockBack();
    }

    private void HandleMovement()
    {
        if (!_isTargetLocked)
        {
            Vector3 movementDirection = (_target.position - transform.position).normalized;

            transform.position += movementDirection * _currentMoveSpeed * Time.deltaTime;
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
            _currentAttackTimer -= Time.deltaTime;

            if (_currentAttackTimer <= 0)
            {
                PlayerController.Instance.TakeDamage(attackDamage);
                _currentAttackTimer = attackSpeed;
            }
        }
    }

    private void HandleKnockBack()
    {
        if (_knockBackTimer > 0)
        {
            _knockBackTimer -= Time.deltaTime;

            if (_currentMoveSpeed > 0)
            {
                _currentMoveSpeed = -_currentMoveSpeed * _knockBackMultiplier;
            }

            if (_knockBackTimer <= 0)
            {
                _knockBackTimer = 0;
                _currentMoveSpeed = moveSpeed;
            }
        }
    }
    
    public void TakeDamage(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0)
        {
            OnTookDamage?.Invoke(this, new OnTookDamageEventArgs{ IsFatal = true });
            StartCoroutine(nameof(Death));
        }
        else
        {
            OnTookDamage?.Invoke(this, new OnTookDamageEventArgs{ IsFatal = false });
        }
    }
    
    public void TakeDamageWithKnockBack(float amount, float knockBackTime, float knockBackMultiplier)
    {
        TakeDamage(amount);
        
        _knockBackTime = knockBackTime;
        _knockBackTimer = _knockBackTime;
        _knockBackMultiplier = knockBackMultiplier;
    }

    private IEnumerator Death()
    {
        yield return new WaitForSeconds(animator.DamageTransitionTime);
        
        Destroy(gameObject);
    }
}
