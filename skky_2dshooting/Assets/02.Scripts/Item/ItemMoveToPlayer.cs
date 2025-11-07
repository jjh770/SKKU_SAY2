using UnityEngine;

public class ItemMoveToPlayer : MonoBehaviour
{
    private float _waitDuration = 2f;

    [Header("베지어 곡선 설정")]
    public float CurveDuration = 2f;
    public float CurveAmount = 0.5f;
    public float CurveHeightAmount = 0.2f;
    public float CurveWidthAmount = 0.3f;

    private GameObject _player;

    private float _curveTime = 0f;
    private bool _isLeft;

    private void Start()
    {
        _player = GameObject.FindWithTag("Player");
        _isLeft = Random.value < 0.5f;
    }
    private void Update()
    {
        _waitDuration -= Time.deltaTime;
        if (_waitDuration <= 0f)
        {
            MoveCurve();
        }
    }

    private void MoveCurve()
    {
        if (_player == null) return;

        _curveTime += Time.deltaTime;
        float t = _curveTime / CurveDuration;

        if (t >= 1f)
        {
            return;
        }

        Vector3 prevPos = transform.position;
        Vector3 targetPos = _player.transform.position;

        Vector3 p0 = prevPos; // 시작점
        Vector3 p3 = targetPos; // 끝점

        Vector3 p1 = prevPos + Vector3.up * CurveHeightAmount + Vector3.right * (_isLeft ? -CurveWidthAmount * 0.6f : CurveWidthAmount * 0.6f);
        Vector3 p2 = targetPos + Vector3.up * CurveHeightAmount + Vector3.right * (_isLeft ? -CurveWidthAmount * 0.3f : CurveWidthAmount * 0.3f);

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
