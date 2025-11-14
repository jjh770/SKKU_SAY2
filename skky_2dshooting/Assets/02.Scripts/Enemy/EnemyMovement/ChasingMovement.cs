using UnityEngine;

public class ChasingMovement : EnemyMovement
{
    private GameObject _target = null;
    private float _distance;
    private bool _isChasing = true;
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
        _distance = Vector2.Distance(targetPosition, myPosition);

        if (_isChasing && _distance > 2f)
        {
            _direction = targetPosition - myPosition;
            _direction.Normalize();

            float angle = Mathf.Atan2(_direction.x, -_direction.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else if (_isChasing && _distance <= 2f)
        {
            _isChasing = false;
        }
    }

}
