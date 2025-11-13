using System.Collections.Generic;
using UnityEngine;
public enum BulletType
{
    Bullet,
    Sub,
    Pet,
}
public class BulletFactory : MonoBehaviour
{
    public static BulletFactory Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        PoolInit();
    }
    [Header("필살기 프리팹")] // 필살기는 어차피 1개
    [SerializeField] private GameObject BoomPrefab;

    [System.Serializable]
    public class PoolInfo
    {
        public BulletType Type;
        public GameObject Prefab;
        public int poolSize = 5;
    }

    [SerializeField] private List<PoolInfo> poolInfos;
    private Dictionary<BulletType, Queue<GameObject>> _typePools;
    

    private void PoolInit()
    {
        _typePools = new Dictionary<BulletType, Queue<GameObject>>();

        foreach (var info in poolInfos)
        {
            Queue<GameObject> bulletPool = new Queue<GameObject>();

            for (int i = 0; i < info.poolSize; i++)
            {
                GameObject bulletObject = Instantiate(info.Prefab, transform);
                bulletObject.SetActive(false);
                bulletPool.Enqueue(bulletObject);
            }
            _typePools.Add(info.Type, bulletPool);
        }
    }

    public GameObject MakeBullet(BulletType type, Vector3 position)
    {
        if (!_typePools.ContainsKey(type)) return null;

        Queue<GameObject> bulletPool = _typePools[type];
        GameObject bulletObject;

        if (bulletPool.Count > 0)
        {
            bulletObject = bulletPool.Dequeue();
        }
        else
        {
            PoolInfo info = poolInfos.Find(x => x.Type == type);
            bulletObject = Instantiate(info.Prefab, transform);
        }
        bulletObject.transform.position = position;
        bulletObject.SetActive(true);
        return bulletObject;
    }

    public void ReturnBullet(BulletType type, GameObject bulletObject)
    {
        if (_typePools.ContainsKey(type))
        {
            bulletObject.transform.rotation = Quaternion.identity;
            bulletObject.transform.SetParent(transform);  

            Rigidbody2D rigidBody = bulletObject.GetComponent<Rigidbody2D>();
            if (rigidBody != null)
            {
                rigidBody.linearVelocity = Vector2.zero;
                rigidBody.angularVelocity = 0f;
            }

            bulletObject.SetActive(false);
            _typePools[type].Enqueue(bulletObject);
        }
    }

    public GameObject MakeBoom(Vector3 position)
    {
        return Instantiate(BoomPrefab, position, Quaternion.identity, transform);
    }
}
