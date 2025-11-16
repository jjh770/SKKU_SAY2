using UnityEngine;

public class BossDelayBullet : MonoBehaviour
{
    [Header("총알 속도")]
    [SerializeField]
    private float _speed = 3f;
    private int _playerHit = 1;
    private Vector2 _moveDirection = Vector2.down; 

    private void Update()
    {
        MoveBullet();
    }

    public void SetDirection(Vector2 direction)
    {
        _moveDirection = direction.normalized;
    }

    private void MoveBullet()
    {
        transform.position += (Vector3)(_moveDirection * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        BulletFactory.Instance.ReturnBullet(EBulletType.BossDirectional, gameObject);

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Hit(_playerHit);
        }
    }
}
