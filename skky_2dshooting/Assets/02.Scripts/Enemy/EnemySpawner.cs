using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("적 오브젝트")]
    public GameObject EnemyPrefab;
    [Header("스폰 대기 시간")]
    public float SpawnDelay = 2f;

    private void Start()
    {
        float randomCoolTime = UnityEngine.Random.Range(1f, 2f);
        SpawnDelay = randomCoolTime;
    }
    private void Update()
    {
        EnemySpawn();    
    }

    private void EnemySpawn()
    {
        SpawnDelay -= Time.deltaTime;
        if (SpawnDelay <= 0f)
        {
            Instantiate(EnemyPrefab, new Vector3(UnityEngine.Random.RandomRange(-1.5f, 1.5f), transform.position.y, 0), Quaternion.EulerAngles(0, 0, 0), transform);
            SpawnDelay = UnityEngine.Random.Range(1f, 3f); ;
            Debug.Log($"{SpawnDelay}초만에 적 스폰");
        }
    }
}
