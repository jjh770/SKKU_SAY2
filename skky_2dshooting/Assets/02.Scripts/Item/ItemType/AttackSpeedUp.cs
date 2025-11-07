using UnityEngine;

public class AttackSpeedUp : MonoBehaviour
{
    private float _attackSpeedValue = 0.5f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerFire playerFire = collision.gameObject.GetComponent<PlayerFire>();
            playerFire.AttackSpeedUp(_attackSpeedValue);
            Destroy(this.gameObject);
        }
    }
}
