using UnityEngine;

public class SpinWeapon : MonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] private float rotationSpeed = 200f;

    private void Update()
    {
        holder.rotation =
            Quaternion.Euler(0f, 0f, holder.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));
    }
}
