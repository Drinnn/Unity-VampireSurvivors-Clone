using System;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float knockBackTime = .5f;
    [SerializeField] private float knockBackMultiplier = 2f;

    private float _amount;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((targetLayerMask.value & (1 << other.transform.gameObject.layer)) != 0)
        {
            if (knockBackTime > 0)
            {
                other.GetComponent<IDamageable>().TakeDamageWithKnockBack(_amount, knockBackTime, knockBackMultiplier);
            }
            else
            {
                other.GetComponent<IDamageable>().TakeDamage(_amount);
            }
        }
    }

    public void SetAmount(float amount)
    {
        _amount = amount;
    }
}
