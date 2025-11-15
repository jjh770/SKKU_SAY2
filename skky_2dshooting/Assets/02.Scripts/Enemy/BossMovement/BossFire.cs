using System.Collections;
using UnityEngine;

public class BossFire : MonoBehaviour
{
    [Header("DirectionalBullet 총구 프리팹 2개")]
    public Transform BossFirePositionLeft_Left;
    public Transform BossFirePositionLeft_Right;
    public Transform BossFirePositionRight_Left;
    public Transform BossFirePositionRight_Right;

    [Header("쿨다운")]
    public float DirectionalCoolTime = 1.5f;
    public float CircleCoolTime = 4f;
    public float DelayCoolTime = 5.0f;
    private float _directionalCoolTimer;
    private float _circleCoolTimer;
    private float _delayCoolTimer;

    [Header("사운드")]
    public AudioClip DirectionalBulletSound;
    public AudioClip CircleBulletSound;

    private bool _startFire = false;

    private void Update()
    {
        if (!_startFire) return;
        BossCoolDown();
    }

    private void BossCoolDown()
    {
        _directionalCoolTimer -= Time.deltaTime;
        _circleCoolTimer -= Time.deltaTime;
        _delayCoolTimer -= Time.deltaTime;

        if (_directionalCoolTimer <= 0f ) 
        {
            _directionalCoolTimer = DirectionalCoolTime;
            StartCoroutine(DirectionalBulletFire());
        }
        if (_circleCoolTimer <= 0f )
        {
            _circleCoolTimer = CircleCoolTime;
            CircleBulletFire(20);
        }
        if (_delayCoolTimer <= 0f )
        {
            _delayCoolTimer = DelayCoolTime;
            StartCoroutine(DelayBulletFire(20, 0.1f));
        }
    }

    private IEnumerator DirectionalBulletFire()
    {
        SoundManager.Instance.PlaySFX(DirectionalBulletSound);

        BulletFactory.Instance.MakeBullet(EBulletType.BossDirectional, BossFirePositionLeft_Right.position);
        BulletFactory.Instance.MakeBullet(EBulletType.BossDirectional, BossFirePositionRight_Left.position);
        yield return new WaitForSeconds(0.5f);
        BulletFactory.Instance.MakeBullet(EBulletType.BossDirectional, BossFirePositionLeft_Left.position);
        BulletFactory.Instance.MakeBullet(EBulletType.BossDirectional, BossFirePositionRight_Right.position);
    }

    private void CircleBulletFire(int bulletCount)
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirXPosition = Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirYPosition = Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveDirection = new Vector3(bulletDirXPosition, bulletDirYPosition, 0f);

            GameObject bullet = BulletFactory.Instance.MakeBullet(EBulletType.BossCircle, transform.position);
            bullet.transform.rotation = Quaternion.Euler(0, 0, -angle);
            bullet.GetComponent<BossCircleBullet>().SetDirection(bulletMoveDirection);

            angle += angleStep;
        }
    }

    private IEnumerator DelayBulletFire(int bulletCount, float delay)
    {
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirXPosition = Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulletDirYPosition = Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 bulletMoveDirection = new Vector2(bulletDirXPosition, bulletDirYPosition).normalized;

            GameObject bullet = BulletFactory.Instance.MakeBullet(EBulletType.BossDelay, transform.position);
            bullet.transform.rotation = Quaternion.Euler(0, 0, -angle);
            bullet.GetComponent<BossDelayBullet>().SetDirection(bulletMoveDirection);

            angle += angleStep;

            yield return new WaitForSeconds(delay);
        }
    }

    public void IsBulletFire(bool isFire)
    {
        _startFire = isFire;
    }
}
