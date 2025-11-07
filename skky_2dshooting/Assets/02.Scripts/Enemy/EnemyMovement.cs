using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    protected float _speed = 3.0f;
    protected Vector3 _direction = Vector3.down;

    private void Update()
    {
        Move();
    }

    protected abstract void Move();
}
