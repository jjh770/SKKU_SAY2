using UnityEngine;

public class MoveSpeedUp : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMove playerMove = collision.gameObject.GetComponent<PlayerMove>();
            playerMove.GetMoveSpeedUp();
            Destroy(this.gameObject);
        }
    }
}
