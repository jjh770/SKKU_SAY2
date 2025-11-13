using UnityEngine;

public class DirectionalMovement : EnemyMovement
{
    [SerializeField]
    private float _directionalMovementSpeed = 0.5f;
    protected override void Move()
    {
        _direction = Vector3.down * _directionalMovementSpeed;
        transform.position += _direction * (_speed * Time.deltaTime);
    }
}
