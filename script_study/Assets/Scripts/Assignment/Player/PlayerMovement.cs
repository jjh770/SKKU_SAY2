using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rigidBody;
    private float currentDirection = 0f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        currentDirection = direction;
    }

    void FixedUpdate()
    {
        rigidBody.linearVelocity = new Vector2(currentDirection * moveSpeed, rigidBody.linearVelocity.y);
    }
}
