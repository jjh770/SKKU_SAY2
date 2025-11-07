using UnityEngine;

public enum EMovementType
{
    DirectionalMovement,
    ChasingMovement,
    RushMovement,
}

public class EnemySpawner : MonoBehaviour
{
    private GameObject _player = null;
    private GameObject _enemy = null;
    [Header("적 프리팹")]
    [SerializeField]
    private GameObject[] _enemyPrefab;

    [Header("스폰 시간")]
    [SerializeField]
    private float _spawnTime = 0.5f;
    private float _spawnTimer = 0.0f;

    [Header("스폰 쿨타임")]
    private float _minSpawnCoolTime = 1.0f;
    private float _maxSpawnCoolTime = 1.5f;

    [Header("스폰 확률")]
    private int _totalWeight = 0;
    private int[] _probabilityWeights = new int[] { 2, 1, 1 };

    [Header("스폰시 위치 오프셋")]
    private float _minSpawnX = -2.5f;
    private float _maxSpawnX = 2.5f;
    private float _minSpawnY = 4.0f;
    private float _maxSpawnY = 5.5f;

    private void Start()
    {
        foreach (int weight in _probabilityWeights)
        {
            _totalWeight += weight;
        }
        _player = GameObject.FindWithTag("Player");
    }

    private void Update()
    {
        if (CanSpawn() == false) return;
        SpawnEnemy();
    }

    private bool CanSpawn()
    {
        _spawnTimer += Time.deltaTime;
        return _spawnTimer >= _spawnTime;
    }

    private void ResetCoolTime()
    {
        _spawnTimer = 0.0f;
        _spawnTime = UnityEngine.Random.Range(_minSpawnCoolTime, _maxSpawnCoolTime);
    }

    private void SpawnEnemy()
    {
        if (_player == null) return;

        ResetCoolTime();
        EMovementType type = GetMovementType();
        if (type == EMovementType.DirectionalMovement)
        {
            _enemy = Instantiate(_enemyPrefab[(int)EMovementType.DirectionalMovement]);
        }
        else if (type == EMovementType.ChasingMovement)
        {
            _enemy = Instantiate(_enemyPrefab[(int)EMovementType.ChasingMovement]);
        }
        else if (type == EMovementType.RushMovement)
        {
            _enemy = Instantiate(_enemyPrefab[(int)EMovementType.RushMovement]);
        }
        _enemy.transform.position = new Vector2(UnityEngine.Random.Range(_minSpawnX, _maxSpawnX), UnityEngine.Random.Range(_minSpawnY, _maxSpawnY));
    }

    private EMovementType GetMovementType()
    {
        float randomValue = UnityEngine.Random.value;
        float totalValue = 0.0f;
        int type = 0;
        for (int i = 0; i < _probabilityWeights.Length; ++i)
        {
            totalValue += (float)_probabilityWeights[i] / _totalWeight;
            if (randomValue < totalValue)
            {
                type = i;
                break;
            }
        }
        return (EMovementType)type;
    }
}
