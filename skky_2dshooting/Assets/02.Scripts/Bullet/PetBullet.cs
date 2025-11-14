using UnityEngine;

public class PetBullet : MonoBehaviour
{
    [SerializeField]
    private float _petBulletSpeed = 10f;
    [SerializeField]
    private float _petBulletPower = 5f;
    private void Update()
    {
        MovePetBullet();
    }

    private void MovePetBullet()
    {
        Vector2 position = transform.position;
        Vector2 direction = Vector2.up;
        Vector2 newPosition = position + direction * _petBulletSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Enemy")) return;

        BulletFactory.Instance.ReturnBullet(EBulletType.Pet, gameObject);

        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Hit(_petBulletPower);
        }
    }
}
