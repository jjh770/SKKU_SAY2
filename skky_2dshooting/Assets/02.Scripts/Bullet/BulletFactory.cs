using Unity.VisualScripting;
using UnityEngine;

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
    }
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject SubBulletPrefab;
    [SerializeField] private GameObject BoomPrefab;
    [SerializeField] private GameObject PetBulletPrefab;

    [Header("풀링")] public int PoolSize = 30;

    public GameObject MakeBullet(Vector3 position)
    {
        return Instantiate(BulletPrefab, position, Quaternion.identity);
    }

    public GameObject MakeSubBullet(Vector3 position)
    {
        return Instantiate(SubBulletPrefab, position, Quaternion.identity);
    }

    public GameObject MakeBoom(Vector3 position)
    {
        return Instantiate(BoomPrefab, position, Quaternion.identity);
    }

    public GameObject MakePetBullet(Vector3 position)
    {
        return Instantiate(PetBulletPrefab, position, Quaternion.identity);
    }

}
