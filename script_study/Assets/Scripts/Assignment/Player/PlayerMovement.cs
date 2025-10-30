using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float currentDirection = 0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Move(float direction)
    {
        currentDirection = direction;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(currentDirection * moveSpeed, rb.linearVelocity.y);
    }
}
