using UnityEngine;

public class PetFire : MonoBehaviour
{
    [Header("펫 총알 프리팹")]
    [SerializeField]
    private GameObject PetBulletPrefab;
    [Header("펫 총구 프리팹")]
    [SerializeField]
    private Transform PetFirePosition;
    [Header("펫 쿨다운")]
    [Range(0.5f, 1.5f)]
    [SerializeField]
    private float PetCoolTime = 2f;
    private float _petCoolTimer;

    private void Start()
    {
        _petCoolTimer = PetCoolTime;
    }
    private void Update()
    {
        PetCoolDown();
    }
    private void PetCoolDown()
    {
        _petCoolTimer -= Time.deltaTime;
        if (_petCoolTimer <= 0)
        {
            _petCoolTimer = PetCoolTime;
            PetShoot();
        }
    }

    private void PetShoot()
    {
        BulletFactory.Instance.MakeBullet(EBulletType.Pet, PetFirePosition.position);
    }
}
