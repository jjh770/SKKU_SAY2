using UnityEngine;

// 개별 아이템 정보
[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    public int weight = 1; 
}

[CreateAssetMenu(fileName = "New Item Table", menuName = "Game/Item Table")]
public class ItemTable : ScriptableObject
{
    [Header("드롭 설정")]
    [SerializeField, Range(0f, 1f)]
    private float _dropProbability = 0.5f;

    [Header("아이템 리스트")]
    [SerializeField]
    private DropItem[] _dropItems;

    private int _totalWeight = 0;

    // 가중치 합계를 캐싱 (OnValidate는 에디터에서만 호출됨)
    private void OnEnable()
    {
        CalculateTotalWeight();
    }

    private void CalculateTotalWeight()
    {
        _totalWeight = 0;
        foreach (var item in _dropItems)
        {
            _totalWeight += item.weight;
        }
    }

    public GameObject DropItem()
    {
        // 드롭 여부 판정
        if (Random.Range(0f, 1f) > _dropProbability)
            return null;

        // 첫 호출 시 가중치 계산 (런타임용)
        if (_totalWeight < 0)
            CalculateTotalWeight();

        // 가중치 기반 랜덤 선택
        int randomValue = Random.Range(0, _totalWeight);
        int cumulativeWeight = 0;

        foreach (var item in _dropItems)
        {
            cumulativeWeight += item.weight;
            if (randomValue < cumulativeWeight)
            {
                return item.itemPrefab;
            }
        }

        return null;
    }
}
