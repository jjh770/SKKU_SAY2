using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float _health = 3;
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;
    private ScoreManager _scoreManager;

    [SerializeField]
    private AudioSource _gameOverSound;
    private void Start()
    {
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

    private void PlayerDie()
    {
        SoundManager.Instance.PlaySFX(_gameOverSound.clip);
        _scoreManager.PlayerDie();
        _scoreManager.SaveBestScore();
        Destroy(gameObject);
    }
}
