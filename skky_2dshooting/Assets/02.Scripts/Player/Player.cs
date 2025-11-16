using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("체력 설정")]
    [SerializeField] private int _maxHealth = 3;
    private int _currentHealth; 
    
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private ScoreManager _scoreManager;
    private HealthUI _healthUI;

    [SerializeField]
    private AudioClip _gameOverSound;

    private void Start()
    {
        _playerFire = GetComponent<PlayerFire>();
        _playerMove = GetComponent<PlayerMove>();
        _scoreManager = ScoreManager.Instance;
        _healthUI = FindAnyObjectByType<HealthUI>();

        _currentHealth = _maxHealth;
        _healthUI?.InitializeHearts(_maxHealth);
    }
    private void Update()
    {
        ChangeAuto();
    }

    private void ChangeAuto()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _playerFire.AutoMode(true);
            _playerMove.AutoMode(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _playerFire.AutoMode(false);
            _playerMove.AutoMode(false);
        }
    }

    public void HealthyPointUp(int value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Min(_currentHealth, _maxHealth);
        _healthUI?.UpdateHearts(_currentHealth);
    }

    public void Hit(int damage)
    {
        _currentHealth -= damage;

        _healthUI.RemoveHeart(_currentHealth);

        if (_currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void BombHit(int damage)
    {
        if (_currentHealth == 1) return;
        _currentHealth -= damage;
        _healthUI.RemoveHeart(_currentHealth);
    }

    private void PlayerDie()
    {
        SoundManager.Instance.PlaySFX(_gameOverSound);
        _scoreManager.PlayerDie();
        _scoreManager.SaveBestScore();

        // 추가 조작을 막기 위해 관련 컴포넌트 비활성화
        _playerMove.enabled = false;
        _playerFire.enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // 사망 시점의 위치를 기준으로 시퀀스 생성 및 재생
        DOTween.Sequence()
            .Append(transform.DOMoveY(transform.position.y - 1f, 1f))
            .Join(transform.DORotate(new Vector3(0, 0, 1080), 1f, RotateMode.FastBeyond360))
            .Join(GetComponent<SpriteRenderer>().material.DOFade(0, 1f))
            .AppendCallback(() =>
            {
                Destroy(gameObject);
            });
    }
}
