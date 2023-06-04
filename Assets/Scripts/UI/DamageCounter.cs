using TMPro;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float lifetime = 2f;
    [SerializeField] private float floatingSpeed = .5f;

    private float _lifetimeTimer;

    private void Update()
    {
        _lifetimeTimer -= Time.deltaTime;

        if (_lifetimeTimer <= 0)
        {
            DamageCounterUI.Instance.PlaceDamageCounterInPool(this);
        }

        transform.position += Vector3.up * floatingSpeed * Time.deltaTime;
    }

    public void Setup(int damageAmount, Vector3 location)
    {
        _lifetimeTimer = lifetime;

        damageText.text = damageAmount.ToString();
        transform.position = location;
        
        gameObject.SetActive(true);
    }
}
