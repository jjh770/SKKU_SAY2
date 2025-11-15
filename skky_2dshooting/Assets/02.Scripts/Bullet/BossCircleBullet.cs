using UnityEngine;

public class BossCircleBullet : MonoBehaviour
{
    [Header("총알 속도")]
    [SerializeField]
    private float _speed = 3f;
    private float _playerHit = 1f;
    private Vector2 _moveDirection = Vector2.down;
    private void OnEnable()
    {
        _moveDirection = Vector2.down; 
    }

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

        BulletFactory.Instance.ReturnBullet(EBulletType.BossCircle, gameObject);

        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            player.Hit(_playerHit);
        }
    }
}
