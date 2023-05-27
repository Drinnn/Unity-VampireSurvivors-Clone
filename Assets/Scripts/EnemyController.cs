using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1.5f;

    private Rigidbody2D _rigidbody2D;

    private Transform _target;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();

        _target = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        Vector3 movementDirection = (_target.position - transform.position).normalized;
        
        _rigidbody2D.velocity = movementDirection * moveSpeed;
    }
}
