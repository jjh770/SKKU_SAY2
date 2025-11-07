using System.Collections;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _currentSpeed = 1f;

    [Header("총알 속도")]
    [SerializeField] // 유니티가 데이터를 읽고 쓸 수 있게 하겠다.
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
        CheckIsOut();
    }
    private void CheckIsOut()
    {
        if (transform.position.y > GameManager.Instance.CameraHalfHeight + 1f)
        {
            Destroy(this.gameObject);
        }
    }
    private void SetSpeed()
    {
        _speed += Time.deltaTime * ((EndSpeed - StartSpeed) / Acceleration); // 1초당 +1과 같다
        _speed = Mathf.Min(_speed, EndSpeed);
    }
    private void MoveBulletAcceleration()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up; // 위쪽 방향
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;  // 새로운 위치
        transform.position = newPosition;      // 새로운 위치로 갱신
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Destroy(this.gameObject); // 총알 오브젝트 파괴

        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(15f);
        }
    }
}