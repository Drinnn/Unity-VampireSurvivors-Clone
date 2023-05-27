using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _target;

    private void Awake()
    {
        _target = FindObjectOfType<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(_target.position.x, _target.position.y, transform.position.z);
    }
}
