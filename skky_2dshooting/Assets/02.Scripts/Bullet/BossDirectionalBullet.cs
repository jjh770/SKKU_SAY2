using UnityEngine;

public class BossDirectionalBullet : MonoBehaviour
{
    [Header("총알 속도")]
    [SerializeField]
    private float _speed = 4f;
    private float _playerHit = 1f;
    private Vector2 _moveDirection = Vector2.down;

    private void OnEnable()
    {
        transform.rotation = Quaternion.Euler(0, 0, 180f); 
    }

    private void Update()
    {
        MoveBullet();
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
