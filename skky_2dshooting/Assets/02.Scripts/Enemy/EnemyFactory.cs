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
    [System.Serializable]
    public class EnemyPoolInfo
    {
        public EEnemyType Type;
        public GameObject Prefab;
        public int PoolSize = 5;
    }
    [Header("적 프리팹")]
    [SerializeField] private List<EnemyPoolInfo> _enemyInfos;
    private Dictionary<EEnemyType, Queue<GameObject>> _enemyPools;

    private void InitializePools()
    {
        _enemyPools = new Dictionary<EEnemyType, Queue<GameObject>>();

        foreach (var info in _enemyInfos)
        {
            Queue<GameObject> enemyPool = new Queue<GameObject>();
            for (int i = 0; i < info.PoolSize; i++)
            {
                GameObject enemyObject = Instantiate(info.Prefab, transform);
                enemyObject.GetComponent<Enemy>().SetEnemyType(info.Type);
                enemyObject.SetActive(false);
                enemyPool.Enqueue(enemyObject);
            }
            _enemyPools.Add(info.Type, enemyPool);
        }
    }

    public GameObject GetEnemy(EEnemyType enemyType)
    {
        if (!_enemyPools.ContainsKey(enemyType)) return null;

        Queue<GameObject> enemyPool = _enemyPools[enemyType];
        GameObject enemyObject;

        if (_enemyPools[enemyType].Count > 0)
        {
            enemyObject = _enemyPools[enemyType].Dequeue();
        }
        else
        {
            EnemyPoolInfo enemyInfo = _enemyInfos.Find(x => x.Type == enemyType);
            enemyObject = Instantiate(enemyInfo.Prefab, transform);
            enemyObject.GetComponent<Enemy>().SetEnemyType(enemyType);
        }

        enemyObject.SetActive(true);
        return enemyObject;
    }
    public void ReturnEnemy(EEnemyType enemyType, GameObject enemy)
    {
        enemy.SetActive(false);
        _enemyPools[enemyType].Enqueue(enemy);
    }
}
