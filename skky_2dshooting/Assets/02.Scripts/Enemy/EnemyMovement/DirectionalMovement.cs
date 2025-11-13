using UnityEngine;

public class DirectionalMovement : EnemyMovement
{
    [SerializeField]
    private float _directionalMovementSpeed = 0.5f;
    protected override void Move()
    {
        transform.position += _direction * _directionalMovementSpeed * _speed * Time.deltaTime;
    }
}
