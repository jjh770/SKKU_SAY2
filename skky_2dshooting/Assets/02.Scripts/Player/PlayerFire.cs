using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
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
    public float MainCoolTime = 0.6f;
    public float SubCoolTime = 0.4f;
    private float _mainCoolTimer;
    private float _subCoolTimer;

    private bool _isAttackSpeedUp = false;
    private float _speedMultiflier = 0.7f;
    private float _attackSpeedUpTimer = 5f;
    private float _startMainCoolTime;
    private float _startSubCoolTime;

    private bool _autoFire = true;

    private void Start()
    {
        _startMainCoolTime = MainCoolTime;
        _startSubCoolTime = SubCoolTime;
    }

    private void Update()
    {
        ChangeAuto();
        CoolDown();
        UpdateAttackSpeedUp();
    }
    private void CoolDown()
    {
        _mainCoolTimer -= Time.deltaTime;
        _subCoolTimer -= Time.deltaTime;
        if (_mainCoolTimer <= 0f && (_autoFire || Input.GetKey(KeyCode.Space)))
        {
            _mainCoolTimer = MainCoolTime;
            Fire();
        }

        if (_subCoolTimer <= 0f && (_autoFire || Input.GetKey(KeyCode.Space)))
        {
            _subCoolTimer = SubCoolTime;
            SubFire();
        }
    }
    public void GetAttackSpeedUp()
    {
        _attackSpeedUpTimer = 5f;
        _isAttackSpeedUp = true;
    }

    private void UpdateAttackSpeedUp()
    {
        if (_isAttackSpeedUp)
        {
            _attackSpeedUpTimer -= Time.deltaTime;

            if (_attackSpeedUpTimer > 0f)
            {
                MainCoolTime = Mathf.Min(_startMainCoolTime * _speedMultiflier, _startMainCoolTime);
                SubCoolTime = Mathf.Min(_startSubCoolTime * _speedMultiflier, _startSubCoolTime);
            }
            else
            {
                MainCoolTime = _startMainCoolTime;
                SubCoolTime = _startSubCoolTime;
                _isAttackSpeedUp = false;
            }
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
        GameObject subBulletLeft = Instantiate(SubBulletPrefab, SubFirePositionLeft.position, Quaternion.identity);
        subBulletLeft.GetComponent<SubBullet>().IsLeft = true;
        GameObject subBulletRight = Instantiate(SubBulletPrefab, SubFirePositionRight.position, Quaternion.identity);
        subBulletRight.GetComponent<SubBullet>().IsLeft = false;
    }
    private void Fire()
    {
        GameObject bulletLeft = Instantiate(BulletPrefab, FirePositionLeft.position, Quaternion.identity);
        GameObject bulletRight = Instantiate(BulletPrefab, FirePositionRight.position, Quaternion.identity);
    }
}
