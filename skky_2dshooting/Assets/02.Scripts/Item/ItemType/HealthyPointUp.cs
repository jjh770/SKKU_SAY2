using UnityEngine;

public class HealthyPointUp : MonoBehaviour
{
    private int _healthyPointValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.HealthyPointUp(_healthyPointValue);
            Destroy(this.gameObject);
        }
    }
}
