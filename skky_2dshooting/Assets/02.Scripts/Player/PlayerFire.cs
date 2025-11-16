using UnityEngine;

public class PlayerFire : MonoBehaviour
{
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

    [Header("데미지 부스트")]
    [SerializeField] private float _damageBoostMultiplier = 2f;
    [SerializeField] private float _damageBoostDuration = 5f;
    private bool _isDamageBoostActive = false;
    private float _damageBoostTimer;

    private bool _isAttackSpeedUp = false;
    private float _speedMultiflier = 0.7f;

    private float _attackSpeedUpTimer = 5f;
    private float _startMainCoolTime;
    private float _startSubCoolTime;

    private bool _autoFire = true;
    private bool _fireButtonPressed = false;
    private bool _bombButtonPressed = false;

    [Header("사운드")]
    public AudioClip MainBulletSound;
    public AudioClip SubBulletSound;
    public AudioClip BoomSound;

    private Player _player;

    private void Start()
    {
        _startMainCoolTime = MainCoolTime;
        _startSubCoolTime = SubCoolTime;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        CoolDown();
        UpdateAttackSpeedUp();
        UpdateDamageBoost();
        _fireButtonPressed = false;
        _bombButtonPressed = false;
    }

    private void CoolDown()
    {
        _mainCoolTimer -= Time.deltaTime;
        _subCoolTimer -= Time.deltaTime;
        _boomCoolTimer -= Time.deltaTime;

        bool canFire = _autoFire || Input.GetKey(KeyCode.Space) || _fireButtonPressed;
        if (_mainCoolTimer <= 0f && canFire)
        {
            _mainCoolTimer = MainCoolTime;
            Fire();
        }
        if (_subCoolTimer <= 0f)
        {
            _subCoolTimer = SubCoolTime;
            SubFire();
        }
        if (_boomCoolTimer <= 0f && (Input.GetKey(KeyCode.Alpha3) || _bombButtonPressed))
        {
            _boomCoolTimer = BoomCoolTime;
            BoomFire();
        }
    }
    private void UpdateDamageBoost()
    {
        if (_isDamageBoostActive)
        {
            _damageBoostTimer -= Time.deltaTime;

            if (_damageBoostTimer <= 0f)
            {
                _isDamageBoostActive = false;
                UpdateAllBulletsDamage(1f);  // 원래 데미지로
            }
        }
    }
    public void ActivateDamageBoost()
    {
        if (_isDamageBoostActive)
        {
            // 이미 활성화 중이면 타이머만 리셋
            _damageBoostTimer = _damageBoostDuration;
        }
        else
        {
            _isDamageBoostActive = true;
            _damageBoostTimer = _damageBoostDuration;
            UpdateAllBulletsDamage(_damageBoostMultiplier);
        }
    }

    // 화면의 모든 총알 데미지 업데이트
    private void UpdateAllBulletsDamage(float multiplier)
    {
        // 모든 IBullet 인터페이스를 가진 오브젝트 찾기
        IBullet[] bullets = FindObjectsOfType<MonoBehaviour>() as IBullet[];

        // Bullet
        Bullet[] mainBullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in mainBullets)
        {
            bullet.SetDamageMultiplier(multiplier);
        }

        // SubBullet
        SubBullet[] subBullets = FindObjectsOfType<SubBullet>();
        foreach (var bullet in subBullets)
        {
            bullet.SetDamageMultiplier(multiplier);
        }
    }

    public void OnFireButtonPressed()
    {
        _fireButtonPressed = true;
    }

    public void OnBombButtonPressed()
    {
        _bombButtonPressed = true;
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
        SoundManager.Instance.PlaySFX(BoomSound);
        BulletFactory.Instance.MakeBullet(EBulletType.Boom, Vector3.zero);
        _player.BombHit(1);
    }

    public void AutoMode(bool isThatAuto)
    {
        _autoFire = isThatAuto;
    }

    private void SubFire()
    {
        SoundManager.Instance.PlaySFX(SubBulletSound);
        GameObject subBulletLeft = BulletFactory.Instance.MakeBullet(EBulletType.Sub, SubFirePositionLeft.position);
        GameObject subBulletRight = BulletFactory.Instance.MakeBullet(EBulletType.Sub, SubFirePositionRight.position);
        subBulletLeft.GetComponent<SubBullet>().IsLeft = true;
        subBulletRight.GetComponent<SubBullet>().IsLeft = false;

        if (_isDamageBoostActive)
        {
            subBulletLeft.GetComponent<SubBullet>().SetDamageMultiplier(_damageBoostMultiplier);
            subBulletRight.GetComponent<SubBullet>().SetDamageMultiplier(_damageBoostMultiplier);
        }
    }

    public void Fire()
    {
        SoundManager.Instance.PlaySFX(MainBulletSound);
        GameObject bulletLeft = BulletFactory.Instance.MakeBullet(EBulletType.Bullet, FirePositionLeft.position);
        GameObject bulletRight = BulletFactory.Instance.MakeBullet(EBulletType.Bullet, FirePositionRight.position);

        if (_isDamageBoostActive)
        {
            bulletLeft.GetComponent<Bullet>().SetDamageMultiplier(_damageBoostMultiplier);
            bulletRight.GetComponent<Bullet>().SetDamageMultiplier(_damageBoostMultiplier);
        }
    }
}
