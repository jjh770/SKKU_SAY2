using System.Collections;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    [Header("총알 프리팹")]
    public GameObject BulletPrefab;
    public GameObject SubBulletPrefab;
    public GameObject BoomPrefab;

    [Header("메인 총구 프리팹")]
    public Transform FirePositionLeft;
    public Transform FirePositionRight;

    [Header("서브 총구 프리팹")]
    public Transform SubFirePositionLeft;
    public Transform SubFirePositionRight;

    [Header("쿨다운")]
    public float BoomCoolTime = 10f;
    public float MainCoolTime = 0.6f;
    public float SubCoolTime = 0.4f;
    private float _boomCoolTimer;
    private float _mainCoolTimer;
    private float _subCoolTimer;

    private bool _isAttackSpeedUp = false;
    private float _speedMultiflier = 0.7f;
    private float _attackSpeedUpTimer = 5f;
    private float _startMainCoolTime;
    private float _startSubCoolTime;

    private bool _autoFire = true;

    [Header("사운드")]
    public AudioSource MainBulletSound;
    public AudioSource SubBulletSound;
    public AudioSource BoomSound;

    private void Start()
    {
        _startMainCoolTime = MainCoolTime;
        _startSubCoolTime = SubCoolTime;
    }

    private void Update()
    {
        CoolDown();
        UpdateAttackSpeedUp();
    }

    private void CoolDown()
    {
        _mainCoolTimer -= Time.deltaTime;
        _subCoolTimer -= Time.deltaTime;
        _boomCoolTimer -= Time.deltaTime;
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
        if (_boomCoolTimer <= 0f && Input.GetKey(KeyCode.Alpha3))
        {
            _boomCoolTimer = BoomCoolTime;
            BoomFire();
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

    private void BoomFire()
    {
        SoundManager.Instance.PlaySFX(BoomSound.clip);
        Instantiate(BoomPrefab, Vector3.zero, Quaternion.identity);
    }

    public void AutoMode(bool isThatAuto)
    {
        _autoFire = isThatAuto;
    }

    private void SubFire()
    {
        SoundManager.Instance.PlaySFX(SubBulletSound.clip);
        GameObject subBulletLeft = Instantiate(SubBulletPrefab, SubFirePositionLeft.position, Quaternion.identity);
        subBulletLeft.GetComponent<SubBullet>().IsLeft = true;
        GameObject subBulletRight = Instantiate(SubBulletPrefab, SubFirePositionRight.position, Quaternion.identity);
        subBulletRight.GetComponent<SubBullet>().IsLeft = false;
    }

    private void Fire()
    {
        SoundManager.Instance.PlaySFX(MainBulletSound.clip);
        GameObject bulletLeft = Instantiate(BulletPrefab, FirePositionLeft.position, Quaternion.identity);
        GameObject bulletRight = Instantiate(BulletPrefab, FirePositionRight.position, Quaternion.identity);
    }
}
