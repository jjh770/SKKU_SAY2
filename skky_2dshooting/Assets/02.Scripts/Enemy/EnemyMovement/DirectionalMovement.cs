using UnityEngine;

public class DirectionalMovement : EnemyMovement
{
    protected override void Move()
    {
        _direction = Vector3.down;
        transform.position += _direction * (_speed * Time.deltaTime);
    }
}
