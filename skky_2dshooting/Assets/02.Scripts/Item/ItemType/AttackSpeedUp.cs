using UnityEngine;

public class AttackSpeedUp : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerFire playerFire = collision.gameObject.GetComponent<PlayerFire>();
            playerFire.GetAttackSpeedUp();
            Destroy(this.gameObject);
        }
    }
}
