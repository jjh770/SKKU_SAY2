using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float _health = 3;
    private PlayerFire _playerFire;
    private PlayerMove _playerMove;

    private void Start()
    {
        _playerFire = GetComponent<PlayerFire>();
        _playerMove = GetComponent<PlayerMove>();
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
            Destroy(gameObject);
        }
    }
}
