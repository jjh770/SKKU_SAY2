using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour
{
    [SerializeField] private float duration = 4f;
    [SerializeField] private float waitDuration = 1f;
    [SerializeField] private float maxScale = 1.5f;
    [SerializeField] private AnimationCurve scaleCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private void Awake()
    {
        transform.localScale = Vector3.zero;
        StartCoroutine(BoostingCoroutine());
    }

    private IEnumerator BoostingCoroutine()
    {
        float halfDuration = duration / 2f;
        float timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            float curveT = scaleCurve.Evaluate(t); 
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * maxScale, curveT);
            yield return null;
        }

        timer = 0f;

        while (timer < waitDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        timer = 0f;

        while (timer < halfDuration)
        {
            timer += Time.deltaTime;
            float t = timer / halfDuration;
            float curveT = scaleCurve.Evaluate(t); 
            transform.localScale = Vector3.Lerp(Vector3.one * maxScale, Vector3.zero, curveT);
            yield return null;
        }

        transform.localScale = Vector3.zero;
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(999f);
        }
    }
}
