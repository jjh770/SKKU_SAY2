using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour
{
    [SerializeField] private float _duration = 3.5f;
    [SerializeField] private float _waitDuration = 1f;
    [SerializeField] private float _maxScale = 1.5f;
    [SerializeField] private AnimationCurve _scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private float _boomDamage = 999f;
    
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(BoostingCoroutine());
    }

    private IEnumerator BoostingCoroutine()
    {
        float halfDuration = _duration / 2f;

        yield return ScaleOverTime(Vector3.zero, Vector3.one * _maxScale, halfDuration);
        yield return new WaitForSeconds(_waitDuration);
        yield return ScaleOverTime(Vector3.one * _maxScale, Vector3.zero, halfDuration);

        Destroy(gameObject);
    }

    private IEnumerator ScaleOverTime(Vector3 startScale, Vector3 endScale, float time)
    {
        float timer = 0f;
        while (timer < time)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            float curveT = _scaleCurve.Evaluate(t);
            transform.localScale = Vector3.LerpUnclamped(startScale, endScale, curveT);
            yield return null;
        }
        transform.localScale = endScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
        {
            enemy.Hit(_boomDamage);
        }
    }
}
