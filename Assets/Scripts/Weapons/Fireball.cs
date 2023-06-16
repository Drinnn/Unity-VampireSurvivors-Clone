using UnityEngine;

public class Fireball : MonoBehaviour, IWeaponObject
{
    [SerializeField] private float growSpeed = 3f;

    private float _rotationSpeed;
    private float _range;
    private float _lifetime;

    private float _rotationAngle;
    private float _lifetimeTimer;
    
    private Vector3 _targetSize;

    private void Awake()
    {
        _targetSize = transform.localScale;
        transform.localScale = Vector3.zero;
    }
    
    public void Setup(float speed, float damage, float range, float duration)
    {
        _rotationSpeed = speed;
        _range = range;
        _lifetime = duration;
        _lifetimeTimer = duration;
        
        GetComponent<Damager>().SetAmount(damage);
    }

    private void Update()
    {
        HandleRotation();
        HandleGrow();
        HandleDestroy();
    }

    private void HandleRotation()
    {
        _rotationAngle += Time.deltaTime * (Mathf.PI / 180f) * _rotationSpeed;
        transform.position = new Vector3(_range * Mathf.Cos(_rotationAngle), _range * Mathf.Sin(_rotationAngle), 0f) + PlayerController.Instance.transform.position;
    }

    private void HandleGrow()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale, _targetSize, growSpeed * Time.deltaTime);
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
