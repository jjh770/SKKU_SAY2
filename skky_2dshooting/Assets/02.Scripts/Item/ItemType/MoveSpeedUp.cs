using UnityEngine;

public class MoveSpeedUp : MonoBehaviour
{
    private int _moveSpeedValue = 5;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            playerMove.MoveSpeedUp(_moveSpeedValue);
            Destroy(this.gameObject);
        }
    }
}
