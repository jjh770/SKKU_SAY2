using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 스탯")]
    public float Speed;
    public float Health = 100f;
    [Header("추가 충돌 구역")]
    public Collider2D LeftCollider;
    public Collider2D RightCollider;
    [Header("적 충돌 데미지")]
    public float Damage = 1f;
    private Vector2 _direction = Vector2.down;

    private void Start()
    {
        _direction = Vector2.down;
    }
    private void Update()
    {
        MovingEnemy();
        CheckIsOut();
    }
    private void CheckIsOut()
    {
        if (transform.position.y < (-1) * GameManager.Instance.CameraHalfHeight - 1f)
        {
            Destroy(this.gameObject);
        }
    }
    public void Hit(float damage, Collider2D collision)
    {
        if (collision == LeftCollider || collision == RightCollider)
        {
            Health -= 6f * 0.8f;
        }
        else
        {
            Health -= 6f;
        }
        if (Health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 몬스터는 플레이어만 죽인다.
        if (!collision.gameObject.CompareTag("Player")) return;
        Player player = collision.gameObject.GetComponent<Player>();
        player.Hit(Damage);
        // Destroy(this.gameObject); // 나 사망
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
    }
    private void MovingEnemy()
    {
        transform.Translate(_direction * (Speed * Time.deltaTime));
    }
}
