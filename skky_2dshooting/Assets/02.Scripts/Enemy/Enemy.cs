using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("적 속도")]
    public float Speed;
    private Vector2 _direction = Vector2.down;

    private void Start()
    {
        _direction = Vector2.down;
    }
    private void Update()
    {
        MovingEnemy();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌 시작");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("충돌 지속");
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("충돌 종료");
    }
    private void MovingEnemy()
    {
        transform.Translate(_direction * (Speed * Time.deltaTime));
    }
}
