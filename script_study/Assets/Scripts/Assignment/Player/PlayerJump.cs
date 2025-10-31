using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckDistance = 0.1f;

    private Rigidbody2D rigidBody;
    private BoxCollider2D boxCollider;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpForce);
        }
    }

    private bool IsGrounded()
    {
        // 박스 콜라이더의 하단 위치와 사이즈 기반으로 체크
        Vector2 boxPosition = (Vector2)transform.position + boxCollider.offset;
        boxPosition.y -= (boxCollider.size.y / 2 + groundCheckDistance);

        Vector2 boxSize = boxCollider.size;

        return Physics2D.OverlapBox(boxPosition, boxSize, 0f, groundLayer) != null;
    }
}
