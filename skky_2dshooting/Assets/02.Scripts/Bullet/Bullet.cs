using System.Collections;
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
        //StartCoroutine(SpeedUp());
    }

    void Update()
    {
        SetSpeed();
        

        MoveBulletAcceleration();
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
        // 충돌한 상대방이 적 태그를 가지고 있다면
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Destroy(this.gameObject); // 총알 오브젝트 파괴
        // GetComponent는 게임 오브젝트에 붙어있는 컴포넌트를 가져올 수 있음.
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        enemy.Health -= 6f;
    }
    IEnumerator SpeedUp()
    {
        while (_currentSpeed < 7f)
        {
            _currentSpeed += 1f; // 속도 증가
            yield return new WaitForSeconds(0.171f); // 1초 대기
        }
        _currentSpeed = 7f; // 최대 속도 설정
    }
    private void MoveBullet()
    {
        Vector2 position = transform.position;
        int scalar = 1; // 속도 조절용 스칼라 값
        Vector2 direction = Vector2.up; // 위쪽 방향

        Vector2 newPosition = position + direction * _currentSpeed * Time.deltaTime;  // 새로운 위치
        transform.position = newPosition;      // 새로운 위치로 갱신
    }
}