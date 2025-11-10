using UnityEngine;

public class RushMovement : EnemyMovement
{
    private float _createTime;
    private float _waitTime = 3.0f;
    private float _defaultSpeed = 10f;
    private void Start()
    {
        _speed = _defaultSpeed;
        _createTime = Time.time;
    }

    private void SetDirection()
    {
        GameObject target = GameObject.FindWithTag("Player");
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 myPosition = transform.position;
        Vector3 targetPosition = target.transform.position;
        _direction = targetPosition - myPosition;
        _direction.Normalize();
        transform.up = _direction;
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
