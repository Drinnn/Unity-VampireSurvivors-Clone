using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float lifetime = 2f;

    private float _lifetimeTimer;

    private void Update()
    {
        _lifetimeTimer -= Time.deltaTime;

        if (_lifetimeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int damageAmount)
    {
        _lifetimeTimer = lifetime;

        damageText.text = damageAmount.ToString();
    }
}
