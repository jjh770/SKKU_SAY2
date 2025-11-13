using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _currentSpeed = 1f;

    [Header("총알 속도")]
    [SerializeField] 
    private float _speed = 1;
    [Header("총알 시작, 종료 속도")]
    public float StartSpeed = 1;
    public float EndSpeed = 7;
    [Header("총알 가속도")]
    public float Acceleration = 1.2f;

    void Start()
    {
        _speed = StartSpeed;
    }

    void Update()
    {
        SetSpeed();
        MoveBulletAcceleration();
    }
    private void SetSpeed()
    {
        _speed += Time.deltaTime * ((EndSpeed - StartSpeed) / Acceleration); 
        _speed = Mathf.Min(_speed, EndSpeed);
    }
    private void MoveBulletAcceleration()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up; 
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;  
        transform.position = newPosition;     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        BulletFactory.Instance.ReturnBullet(BulletType.Bullet, gameObject);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(20f);
        }
    }
}