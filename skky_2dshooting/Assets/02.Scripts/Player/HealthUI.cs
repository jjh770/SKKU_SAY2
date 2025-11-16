using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class HealthUI : MonoBehaviour
{
    [Header("하트 설정")]
    [SerializeField] private GameObject heartPrefab;  
    [SerializeField] private Transform heartContainer;
    [SerializeField] private Sprite fullHeart;  
    [SerializeField] private Sprite emptyHeart; 

    private List<Image> _hearts = new List<Image>();

    // 최대 체력으로 하트 생성
    public void InitializeHearts(int maxHealth)
    {
        // 기존 하트 제거
        ClearHearts();

        // 최대 체력만큼 하트 생성
        for (int i = 0; i < maxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartContainer);
            Image heartImage = heart.GetComponent<Image>();
            _hearts.Add(heartImage);
        }
    }

    // ⭐ 현재 체력에 맞게 하트 업데이트
    public void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < _hearts.Count; i++)
        {
            if (i < currentHealth)
            {
                // 체력이 있는 하트
                _hearts[i].sprite = fullHeart;
                _hearts[i].enabled = true;
            }
            else
            {
                // 체력이 없는 하트 (숨기거나 빈 하트 표시)
                if (emptyHeart != null)
                {
                    _hearts[i].sprite = emptyHeart;
                }
                else
                {
                    _hearts[i].enabled = false;  // 빈 하트 이미지 없으면 숨김
                }
            }
        }
    }

    // 하트 하나 제거 애니메이션
    public void RemoveHeart(int heartIndex)
    {
        if (heartIndex >= 0 && heartIndex < _hearts.Count)
        {
            // DOTween 애니메이션 (선택사항)
            _hearts[heartIndex].transform.DOPunchScale(Vector3.one * 0.3f, 0.3f)
                .OnComplete(() =>
                {
                    if (emptyHeart != null)
                    {
                        _hearts[heartIndex].sprite = emptyHeart;
                    }
                    else
                    {
                        _hearts[heartIndex].enabled = false;
                    }
                });
        }
    }

    private void ClearHearts()
    {
        foreach (var heart in _hearts)
        {
            Destroy(heart.gameObject);
        }
        _hearts.Clear();
    }
}
