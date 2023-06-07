using System;
using UnityEngine;

public class ExpPickup : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float xpAmount = 1f;
    [SerializeField] private float pickupDistance = 1.5f;
    [SerializeField] private float timeBetweenChecks = .2f;
    [SerializeField] private float moveSpeed = 2f;

    private PlayerController _playerController;    
    
    private bool _movingToPlayer;
    private float _checkTimer;

    private void Start()
    {
        _playerController = PlayerController.Instance;
    }

    private void Update()
    {
        if (_movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, _playerController.transform.position,
                moveSpeed * Time.deltaTime);
        }
        else
        {
            _checkTimer -= Time.deltaTime;
            if (_checkTimer <= 0)
            {
                _checkTimer = timeBetweenChecks;
                if (Vector3.Distance(transform.position, _playerController.transform.position) < pickupDistance)
                {
                    _movingToPlayer = true;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayerMask.value & (1 << other.transform.gameObject.layer)) != 0)
        {
            ExpSystem.Instance.AddXp(xpAmount);
            Destroy(gameObject);    
        }
    }
}
