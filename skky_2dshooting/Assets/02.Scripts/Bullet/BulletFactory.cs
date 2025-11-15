using System.Collections.Generic;
using UnityEngine;
public enum EBulletType
{
    Bullet,
    Sub,
    Pet,
    Boom,
    BossDirectional,
    BossCircle,
    BossDelay,
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

    [System.Serializable]
    public class PoolInfo
    {
        public EBulletType Type;
        public GameObject Prefab;
        public int PoolSize = 10;
    }

    [SerializeField] private List<PoolInfo> _poolInfos;
    private Dictionary<EBulletType, Queue<GameObject>> _typePools;
    

    private void PoolInit()
    {
        _typePools = new Dictionary<EBulletType, Queue<GameObject>>();

        foreach (var info in _poolInfos)
        {
            Queue<GameObject> bulletPool = new Queue<GameObject>();

            for (int i = 0; i < info.PoolSize; i++)
            {
                GameObject bulletObject = Instantiate(info.Prefab, transform);

                bulletObject.SetActive(false);
                bulletPool.Enqueue(bulletObject);
            }
            _typePools.Add(info.Type, bulletPool);
        }
    }

    public GameObject MakeBullet(EBulletType type, Vector3 position)
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
            PoolInfo info = _poolInfos.Find(x => x.Type == type);
            bulletObject = Instantiate(info.Prefab, transform);
        }
        bulletObject.transform.position = position;
        bulletObject.SetActive(true);
        return bulletObject;
    }

    public void ReturnBullet(EBulletType type, GameObject bulletObject)
    {
        if (!_typePools.ContainsKey(type)) return;

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
