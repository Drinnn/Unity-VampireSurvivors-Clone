using UnityEngine;

public class Fireball : MonoBehaviour, IWeaponObject
{
    [SerializeField] private float growSpeed = 3f;

    private float _lifetime;
    private float _lifetimeTimer;
    private Vector3 _targetSize;

    private void Awake()
    {
        _targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growSpeed * Time.deltaTime);

        HandleDestroy();
    }

    public void Setup(float lifetime)
    {
        _lifetime = lifetime;
        _lifetimeTimer = _lifetime;
    }

    private void HandleDestroy()
    {
        _lifetimeTimer -= Time.deltaTime;
        if (_lifetimeTimer <= 0)
        {
            _targetSize = Vector3.zero;

            if (transform.localScale.x == 0)
            {
                Destroy(gameObject);
                _lifetimeTimer = _lifetime;
            }
        }
    }
}
