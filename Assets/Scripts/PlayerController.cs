using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.8f;

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        Vector2 playerInput = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(playerInput.x, playerInput.y, 0);

        transform.position += movementDirection * moveSpeed * Time.deltaTime;
    }
}
