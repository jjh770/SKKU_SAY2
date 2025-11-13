using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    [SerializeField] protected GameObject itemEffectPrefab; // 모든 아이템이 공유

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 각 아이템별 고유 기능 실행
            ApplyItemEffect(collision.gameObject);
            ApplyItemSound();
            // 이펙트 생성 (공통)
            if (itemEffectPrefab != null)
            {
                GameObject effect = Instantiate(itemEffectPrefab, transform.position, Quaternion.identity);
                Destroy(effect, 2f);
            }

            Destroy(this.gameObject);
        }
    }

    // 자식 클래스에서 구현해야 할 추상 메서드
    protected abstract void ApplyItemEffect(GameObject player);
    protected abstract void ApplyItemSound();
}
