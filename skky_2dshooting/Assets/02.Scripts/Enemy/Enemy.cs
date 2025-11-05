using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 스탯")]
    public float Speed;
    public float Health = 100f;

    private Vector2 _direction = Vector2.down;

    private void Start()
    {
        _direction = Vector2.down;
    }
    private void Update()
    {
        MovingEnemy();
        EnemyIsDead();
    }
    private void EnemyIsDead()
    {
        if (Health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 시작");

        // 몬스터는 플레이어만 죽인다.
        if (!collision.gameObject.CompareTag("Player")) return;

        PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
        playerMove.HealthCount--;

        // Destroy(this.gameObject); // 나 사망
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("충돌 지속");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("충돌 종료");
    }
    private void MovingEnemy()
    {
        transform.Translate(_direction * (Speed * Time.deltaTime));
    }
}
