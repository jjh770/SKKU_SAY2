using UnityEngine;

public class Item : MonoBehaviour
{
    private int _value = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            playerMove.SpeedUp(_value);
            Destroy(this.gameObject);
        }
    }
}
