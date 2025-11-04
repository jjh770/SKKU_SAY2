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
    public const float CoolTime = 0.6f;
    private float _coolTimer;

    private bool _autoFire = true;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _autoFire = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _autoFire = false;
        }

        _coolTimer -= Time.deltaTime;
        if (_coolTimer > 0) return;
        if (_autoFire || Input.GetKey(KeyCode.Space))
        {
            _coolTimer = CoolTime;
            Fire();
            SubFire();

        }
    }
    private void SubFire()
    {
        GameObject subBulletLeft = Instantiate(SubBulletPrefab, SubFirePositionLeft.position, Quaternion.EulerAngles(Vector3.zero));
        GameObject subBulletRight = Instantiate(SubBulletPrefab, SubFirePositionRight.position, Quaternion.EulerAngles(Vector3.zero));
    }
    private void Fire()
    {
        GameObject bulletLeft = Instantiate(BulletPrefab, FirePositionLeft.position, Quaternion.EulerAngles(Vector3.zero));
        GameObject bulletRight = Instantiate(BulletPrefab, FirePositionRight.position, Quaternion.EulerAngles(Vector3.zero));
    }
}
