using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 스탯")]
    public float Health = 100f;
    [Header("적 충돌 데미지")]
    public float Damage = 1f;

    private void Update()
    {
        CheckIsOut();
    }
    private void CheckIsOut()
    {
        if (transform.position.y < -GameManager.Instance.CameraHalfHeight - 1f
            || transform.position.x > GameManager.Instance.CameraHalfWidth + 1f
            || transform.position.x < -GameManager.Instance.CameraHalfWidth - 1f)
        {
            Destroy(this.gameObject);
        }
    }
    public void Hit(float damage)
    {
        Health -= damage;
        if (Health <= 0)
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
        Destroy(this.gameObject); // 나 사망
    }
}
