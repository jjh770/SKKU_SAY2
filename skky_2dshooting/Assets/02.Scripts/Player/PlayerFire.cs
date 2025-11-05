using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // 스페이스바를 누르면 총알 발사

    // 필요 속성
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;

    [Header("메인 총구 프리팹")]
    public Transform FirePositionLeft;
    public Transform FirePositionRight;

    [Header("서브 총구 프리팹")]
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("쿨다운")]
    public const float MainCoolTime = 0.6f;
    public const float SubCoolTime = 0.4f;
    private float _mainCoolTimer;
    private float _subCoolTimer;

    private bool _autoFire = true;

    private void Update()
    {
        ChangeAuto();
        CoolDown();
    }
    private void CoolDown()
    {
        _mainCoolTimer -= Time.deltaTime;
        _subCoolTimer -= Time.deltaTime;
        // 메인 쿨다운
        if (_mainCoolTimer <= 0f && (_autoFire || Input.GetKey(KeyCode.Space)))
        {
            _mainCoolTimer = MainCoolTime;
            Fire();
        }

        // 서브 쿨다운
        if (_subCoolTimer <= 0f && (_autoFire || Input.GetKey(KeyCode.Space)))
        {
            _subCoolTimer = SubCoolTime;
            SubFire();
        }
    }

    private void ChangeAuto()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _autoFire = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _autoFire = false;
        }
    }
    private void SubFire()
    {
        GameObject subBulletLeft = Instantiate(SubBulletPrefab, SubFirePositionLeft.position, Quaternion.EulerAngles(Vector3.zero));
        subBulletLeft.GetComponent<SubBullet>().IsLeft = true;
        GameObject subBulletRight = Instantiate(SubBulletPrefab, SubFirePositionRight.position, Quaternion.EulerAngles(Vector3.zero));
        subBulletRight.GetComponent<SubBullet>().IsLeft = false;
    }
    private void Fire()
    {
        GameObject bulletLeft = Instantiate(BulletPrefab, FirePositionLeft.position, Quaternion.EulerAngles(Vector3.zero));
        GameObject bulletRight = Instantiate(BulletPrefab, FirePositionRight.position, Quaternion.EulerAngles(Vector3.zero));
    }
}
