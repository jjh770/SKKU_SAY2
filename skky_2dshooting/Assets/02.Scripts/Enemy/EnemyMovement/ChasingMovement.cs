using UnityEngine;

public class ChasingMovement : EnemyMovement
{
    private GameObject _target = null;
    private void Start()
    {
        _target = GameObject.FindWithTag("Player");
    }

    protected override void Move()
    {
        SetDirection();
        transform.position += _direction * (_speed * Time.deltaTime);
    }

    private void SetDirection()
    {
        if (_target == null) return;
        Vector3 myPosition = transform.position;
        Vector3 targetPosition = _target.transform.position;
        _direction = targetPosition - myPosition;
        _direction.Normalize();
    }
}
