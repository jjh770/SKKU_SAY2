using System.Collections;
using UnityEngine;

public class SubBullet : MonoBehaviour
{

    [Header("총알 속도")]
    [SerializeField] // 유니티가 데이터를 읽고 쓸 수 있게 하겠다.
    private float _speed = 1;
    [Header("총알 시작, 종료 속도")]
    public float StartSpeed = 1;
    public float EndSpeed = 6;
    [Header("총알 가속도")]
    public float Acceleration = 0.7f;

    void Start()
    {
        _speed = StartSpeed;
    }

    void Update()
    {
        _speed += Time.deltaTime * ((EndSpeed - StartSpeed)/ Acceleration); // 1초당 +1과 같다

        _speed = Mathf.Min(_speed, EndSpeed);

        MoveBullet_Acceleration();
        //MoveBullet();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 충돌한 상대방이 적 태그를 가지고 있다면
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Destroy(this.gameObject); // 총알 오브젝트 파괴

        // GetComponent는 게임 오브젝트에 붙어있는 컴포넌트를 가져올 수 있음.
        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (collision == enemy.LeftCollider || collision == enemy.RightCollider)
        {
            enemy.Health -= 4f * 0.8f;
        }
        else
        {
            enemy.Health -= 4f;
        }
    }
    private void MoveBullet_Acceleration()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up; // 위쪽 방향
        Vector2 newPosition = position + direction * _speed * Time.deltaTime;  // 새로운 위치
        transform.position = newPosition;      // 새로운 위치로 갱신
    }
}