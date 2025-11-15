using UnityEngine;

public class DestroyZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.Bullet, other.gameObject);
        }
        else if (other.CompareTag("SubBullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.Sub, other.gameObject);
        }
        else if (other.CompareTag("PetBullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.Pet, other.gameObject);
        }
        else if (other.CompareTag("BossDirectionalBullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.BossDirectional, other.gameObject);
        }
        else if (other.CompareTag("BossCircleBullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.BossCircle, other.gameObject);
        }
        else if (other.CompareTag("BossDelayBullet"))
        {
            BulletFactory.Instance.ReturnBullet(EBulletType.BossDelay, other.gameObject);
        }
        else if (other.CompareTag("Enemy"))
        {
            EEnemyType enemyType = other.GetComponent<Enemy>().GetEnemyType();
            EnemyFactory.Instance.ReturnEnemy(enemyType, other.gameObject);
        }
    }
}