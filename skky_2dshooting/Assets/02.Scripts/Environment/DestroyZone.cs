using System;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.name)
        {
            case "Bullet(Clone)":
                BulletFactory.Instance.ReturnBullet(BulletType.Bullet, other.gameObject);
                break;
            case "SubBullet(Clone)":
                BulletFactory.Instance.ReturnBullet(BulletType.Sub, other.gameObject);
                break;
            case "PetBullet(Clone)":
                BulletFactory.Instance.ReturnBullet(BulletType.Pet, other.gameObject);
                break;
        }
    }
}