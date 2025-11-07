using UnityEngine;

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
        if (Random.Range(0f, 1f) > _dropProbability)
            return null;

        if (_totalWeight < 0)
            CalculateTotalWeight();

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
