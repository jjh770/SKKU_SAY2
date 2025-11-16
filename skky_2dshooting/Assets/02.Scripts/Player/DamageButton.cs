using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DamageButton : MonoBehaviour, IPointerUpHandler
{
    private ScoreManager _scoreManager;
    private PlayerFire _playerFire;
    [SerializeField] private Button damageButton;
    [SerializeField] private float cooldownTime = 10f;
    [SerializeField] private int requiredScore = 3000;

    private bool _canUse = true;

    private void Start()
    {
        _playerFire = FindObjectOfType<PlayerFire>();
        _scoreManager = ScoreManager.Instance;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_canUse) return;

        if (!_scoreManager.TryUseScore(requiredScore)) return;

        // 데미지 부스트 활성화
        _playerFire.ActivateDamageBoost();

        // 쿨다운 시작
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        _canUse = false;
        damageButton.interactable = false;

        float elapsed = 0f;
        while (elapsed < cooldownTime)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        _canUse = true;
        damageButton.interactable = true;
    }
}
