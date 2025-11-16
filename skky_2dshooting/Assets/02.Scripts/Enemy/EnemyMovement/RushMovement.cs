using UnityEngine;

public class RushMovement : EnemyMovement
{
    private float _createTime;
    private float _waitTime = 3.0f;
    private float _defaultSpeed = 10f;
    private Transform _playerTransform;
    private Enemy _enemyComponent;

    private void OnEnable()
    {
        _speed = _defaultSpeed;
        _createTime = Time.time;
        _direction = Vector3.zero;
        GameObject player = GameObject.FindWithTag("Player");
        _playerTransform = player != null ? player.transform : null;
        _enemyComponent = GetComponent<Enemy>();
    }

    private void SetDirection()
    {
        if (_playerTransform == null)
        {
            _enemyComponent.ReturnPool(EEnemyType.RushMovement);
            return;
        }

        Vector3 myPosition = transform.position;
        Vector3 targetPosition = _playerTransform.position;
        _direction = targetPosition - myPosition;
        _direction.Normalize();
        transform.up = -_direction;
    }

    protected override void Move()
    {
        if(CanMove() == false) return;
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private bool CanMove()
    {
        float currentTime = Time.time;
        if (currentTime - _createTime > _waitTime) return true;
        SetDirection();
        return false;
    }
}
