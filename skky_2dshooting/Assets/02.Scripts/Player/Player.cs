using DG.Tweening;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _health = 3;
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private ScoreManager _scoreManager;

    [SerializeField]
    private AudioClip _gameOverSound;

    private DG.Tweening.Sequence _dieSequence;

    private void Start()
    {
        MakeDieSequence();
        _playerFire = GetComponent<PlayerFire>();
        _playerMove = GetComponent<PlayerMove>();
        _scoreManager = FindAnyObjectByType<ScoreManager>();
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
        _health += value;
    }
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            PlayerDie();
        }
    }

    private void MakeDieSequence()
    {
        _dieSequence = DOTween.Sequence()
            .Append(transform.DOMoveY(transform.position.y - 1f, 1f))
            .Join(transform.DORotate(new Vector3(0, 0, 1080), 1f, RotateMode.FastBeyond360))
            .Join(GetComponent<SpriteRenderer>().material.DOFade(0, 1f))
            .AppendCallback(() =>
            {
                Destroy(gameObject);
            })
            .Pause();
    }

    private void PlayerDie()
    {
        SoundManager.Instance.PlaySFX(_gameOverSound);
        _scoreManager.PlayerDie();
        _scoreManager.SaveBestScore();
        _dieSequence.Play();
    }
}
