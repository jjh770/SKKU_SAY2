using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 스탯")]
    public float Health = 100f;
    [Header("적 충돌 데미지")]
    public float Damage = 1f;
    [Header("아이템 드롭")]
    [SerializeField]
    private ItemTable _itemTable;
    [Header("폭발 이펙트")]
    public GameObject ExplosionPrefab;
    [Header("적 사망시 점수")]
    [SerializeField]
    private int _score = 100;
    private bool _isDead = false;

    private Animator _animator;
    private ScoreManager scoreManager;
    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }
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
        if (_isDead) return;

        _animator.SetTrigger("Hit");

        Health -= damage;
        if (Health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (_isDead) return;
        _isDead = true;

        // 응집도를 높혀라
        // 응집도 : "데이터"와 "데이터를 조작하는 로직"이 얼마나 잘 모였나
        // 응집도를 높이로 필요한 것만 외부에 노출시키는 것을 캡슐화
        MakeExplosionEffect();
        Destroy(this.gameObject);

        if (scoreManager.CheckPlayerDead()) return;
        scoreManager.AddScore(_score);

        TryDropItem();
    }

    private void MakeExplosionEffect()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }

    private void TryDropItem()
    {
        if (_itemTable == null) return;

        GameObject droppedItem = _itemTable.DropItem();
        if (droppedItem != null)
        {
            Instantiate(droppedItem, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isDead) return;

        if (!collision.gameObject.CompareTag("Player")) return;
        Player player = collision.gameObject.GetComponent<Player>();
        player.Hit(Damage);
        Die();
    }
}
