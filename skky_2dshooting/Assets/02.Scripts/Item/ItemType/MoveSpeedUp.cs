using UnityEngine;

public class MoveSpeedUp : ItemBase
{
    protected override void ApplyItemEffect(GameObject player)
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        playerMove.GetMoveSpeedUp();
    }
}
