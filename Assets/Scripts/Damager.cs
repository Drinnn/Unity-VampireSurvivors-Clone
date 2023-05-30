using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] private float amount = 10f;
    [SerializeField] private LayerMask targetLayerMask;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((targetLayerMask.value & (1 << other.transform.gameObject.layer)) != 0)
        {
            other.GetComponent<IDamageable>().TakeDamage(amount);
        }
    }
}
