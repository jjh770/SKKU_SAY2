using System.Collections;
using UnityEngine;

public class SubBullet : MonoBehaviour
{
    [Header("총알 속도")]
    [SerializeField]
    private float _speed = 1;
    [Header("총알 시작, 종료 속도")]
    public float StartSpeed = 1;
    public float EndSpeed = 6;
    [Header("총알 가속도")]
    public float Acceleration = 0.7f;
    [Header("좌우 구분")]
    public bool IsLeft;
    [Header("베지어 곡선 설정")]
    public bool useBezierCurve = false;
    public bool curveLeft = false;
    public float curveDuration = 1f;
    public float curveAmount = 3f;

    private Vector3 _startPos;
    private Vector3 _targetPosition ; 
    private float _curveTime = 0f;
    private Vector3 _lastDirection;  // 마지막 이동 방향 저장
    private bool _curveFinished = false;

    void Start()
    {
        _speed = StartSpeed;
        _startPos = transform.position;
        _targetPosition = new Vector3(0, GameManager.Instance.CameraHalfHeight, 0);
    }

    void Update()
    {
        _speed += Time.deltaTime * ((EndSpeed - StartSpeed) / Acceleration);
        _speed = Mathf.Min(_speed, EndSpeed);

        if (!_curveFinished)
        {
            MoveBezier();
        }
        else if (_curveFinished)
        {
            transform.position += _lastDirection * _speed * Time.deltaTime;
        }

        CheckIsOut();
        FindNearestEnemy();
    }

    private void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0) return;

        float nearestDist = float.MaxValue;

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);

            if (dist < nearestDist)
            {
                nearestDist = dist;
                _targetPosition = enemy.transform.position;
            }
        }
    }

    private void CheckIsOut()
    {
        if (GameManager.Instance != null)
        {
            if (transform.position.y > GameManager.Instance.CameraHalfHeight + 1f)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Destroy(this.gameObject);

        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(0.4f, collision);
        }
    }

    private void MoveStraight()
    {
        transform.position += Vector3.up * _speed * Time.deltaTime;
    }

    private void MoveBezier()
    {
        _curveTime += Time.deltaTime;
        float t = _curveTime / curveDuration;

        if (t >= 1f)
        {
            _curveFinished = true;  
            return;
        }

        Vector3 prevPos = transform.position;

        Vector3 mid = (_startPos + _targetPosition) / 2f;
        mid += Vector3.right * (IsLeft ? -curveAmount : curveAmount);

        Vector3 point1 = Vector3.Lerp(_startPos, mid, t);
        Vector3 point2 = Vector3.Lerp(mid, _targetPosition, t);
        transform.position = Vector3.Lerp(point1, point2, t);

        _lastDirection = (transform.position - prevPos).normalized;
    }
}
