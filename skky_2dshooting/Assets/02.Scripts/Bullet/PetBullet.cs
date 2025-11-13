using UnityEngine;

public class PetBullet : MonoBehaviour
{
    private float _petBulletSpeed = 10f;

    private void Update()
    {
        MovePetBullet();
        CheckIsOut();
    }

    private void MovePetBullet()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        Vector2 newPosition = position + direction * _petBulletSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
    private void CheckIsOut()
    {
        if (transform.position.y > GameManager.Instance.CameraHalfHeight + 1f)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        Destroy(this.gameObject);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(5f);
        }
    }
}
