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
    public float CurveDuration = 1f;
    public float CurveAmount = 3f;
    public float CurveHeightAmount = 0.2f;
    public float CurveWidthAmount = 0.3f;

    private Vector3 _targetPosition ; 
    private float _curveTime = 0f;
    private Vector3 _lastDirection;
    private bool _curveFinished = false;

    private void OnEnable()
    {
        _speed = StartSpeed;
        _targetPosition = new Vector3(0, GameManager.Instance.CameraHalfHeight, 0);

        _curveTime = 0f;
        _curveFinished = false;
        _lastDirection = Vector3.up;
    }

    private void Update()
    {
        _speed += Time.deltaTime * ((EndSpeed - StartSpeed) / Acceleration);
        _speed = Mathf.Min(_speed, EndSpeed);

        if (!_curveFinished)
        {
            MoveCurve();
        }
        else if (_curveFinished)
        {
            transform.position += _lastDirection * _speed * Time.deltaTime;
        }

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


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        BulletFactory.Instance.ReturnBullet(EBulletType.Sub, gameObject);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(15f);
        }
    }

    private void MoveCurve()
    {
        _curveTime += Time.deltaTime;
        float t = _curveTime / CurveDuration;

        if (t >= 1f)
        {
            _curveFinished = true;  
            return;
        }

        Vector3 prevPos = transform.position;
        Vector3 targetPos = _targetPosition;

        Vector3 p0 = prevPos;
        Vector3 p3 = targetPos;

        Vector3 p1 = prevPos + -Vector3.up * CurveHeightAmount + Vector3.right * (IsLeft ? -CurveWidthAmount * 0.3f : CurveWidthAmount * 0.3f);
        Vector3 p2 = prevPos + Vector3.up * CurveHeightAmount + Vector3.right * (IsLeft ? -CurveWidthAmount * 0.6f : CurveWidthAmount * 0.6f);

        transform.position = BezierMove(p0, p1, p2, p3, t);
    }

    private Vector3 BezierMove(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
    {
        Vector3 m0 = Vector3.Lerp(p0, p1, t);
        Vector3 m1 = Vector3.Lerp(p1, p2, t);
        Vector3 m2 = Vector3.Lerp(p2, p3, t);

        Vector3 b0 = Vector3.Lerp(m0, m1, t);
        Vector3 b1 = Vector3.Lerp(m1, m2, t);

        return Vector3.Lerp(b0, b1, t);
    }
}
