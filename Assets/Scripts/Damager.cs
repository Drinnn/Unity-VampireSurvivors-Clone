using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float amount = 10f;
    [SerializeField] private LayerMask targetLayerMask;
    [SerializeField] private float knockBackTime = .5f;
    [SerializeField] private float knockBackMultiplier = 2f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((targetLayerMask.value & (1 << other.transform.gameObject.layer)) != 0)
        {
            if (knockBackTime > 0)
            {
                other.GetComponent<IDamageable>().TakeDamageWithKnockBack(amount, knockBackTime, knockBackMultiplier);
            }
            else
            {
                other.GetComponent<IDamageable>().TakeDamage(amount);
            }
        }
    }
}
