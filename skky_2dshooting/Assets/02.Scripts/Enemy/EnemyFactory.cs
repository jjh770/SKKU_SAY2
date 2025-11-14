using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    public static EnemyFactory Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        InitializePools();  
    }

    [Header("적 프리팹")]
    [SerializeField] private GameObject[] _enemyPrefab;
    private Dictionary<EEnemyType, Queue<GameObject>> _enemyPools;

    private void InitializePools()
    {
        _enemyPools = new Dictionary<EEnemyType, Queue<GameObject>>();

        for (int i = 0; i < _enemyPrefab.Length; i++)
        {
            _enemyPools.Add((EEnemyType)i, new Queue<GameObject>());
        }
    }

    public GameObject GetEnemy(EEnemyType enemyType)
    {
        if (_enemyPools[enemyType].Count > 0)
        {
            GameObject enemy = _enemyPools[enemyType].Dequeue();
            enemy.SetActive(true);
            enemy.GetComponent<Enemy>().CheckEnemyType(enemyType);
            return enemy;
        }
        GameObject newEnemy = Instantiate(_enemyPrefab[(int)enemyType], transform);
        newEnemy.GetComponent<Enemy>().CheckEnemyType(enemyType);  // 추가!
        return newEnemy;
    }
    public void ReturnEnemy(EEnemyType enemyType, GameObject enemy)
    {
        enemy.SetActive(false);
        _enemyPools[enemyType].Enqueue(enemy);
    }
}
