using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            BulletFactory.Instance.ReturnBullet(BulletType.Bullet, other.gameObject);
        }
        else if (other.CompareTag("SubBullet"))
        {
            BulletFactory.Instance.ReturnBullet(BulletType.Sub, other.gameObject);
        }
        else if (other.CompareTag("PetBullet"))
        {
            BulletFactory.Instance.ReturnBullet(BulletType.Pet, other.gameObject);
        }
    }
}