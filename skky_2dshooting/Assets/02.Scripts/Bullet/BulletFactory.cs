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
        PoolInit();
    }
    [Header("총알 프리팹")] // 복사해올 총알 프리팹 게임 오브젝트
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] private GameObject SubBulletPrefab;
    [SerializeField] private GameObject BoomPrefab;
    [SerializeField] private GameObject PetBulletPrefab;

    [Header("풀링")] public int PoolSize = 30;
    private GameObject[] _bulletObjectPool; // 게임 내 총알을 담아둘 풀 : 탄창

    // Awake vs Start vs Lazy
    // Awake : 게임이 막 시작될 때 
    // Start : 첫번째 프레임이 호출되기 바로 직전
    private void PoolInit()
    {
        // 1. 탄창을 총알을 담을 수 있는 크기의 배열을 만들어줌.
        _bulletObjectPool = new GameObject[PoolSize];
        // 2. 탄창 크기만큼 반복해서
        for (int i = 0; i < PoolSize; i++)
        {
            // 3. 총알을 생성한다.
            GameObject bulletObject = Instantiate(BulletPrefab);

            // 4. 생성한 총알을 탄창(풀)에 담는다.
            _bulletObjectPool[i] = bulletObject;

            // 5. 비활성화 한다.
            bulletObject.SetActive(false);
        }
    }


    public GameObject MakeBullet(Vector3 position)
    {
        // 필요하다면 여기서 생성 이펙트도 생성하고
        // 필요하다면 인자값으로 데미지도 받아서 넘겨주고

        // 1. 탄창 안에 있는 총알들 중에서
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject bulletObject = _bulletObjectPool[i];

            // 2. 비활성화된 총알 하나를 찾기
            if (bulletObject.activeInHierarchy == false)
            {
                // 3. 위치를 수정하고 활성화시킴
                bulletObject.transform.position = position;
                bulletObject.SetActive(true);

                // 총알을 발사 했으므로 중지
                return bulletObject;
            }
        }

        Debug.LogError("탄창에 총알 개수가 부족합니다.");
        return null;
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
