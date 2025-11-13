using UnityEngine;

public class MoveSpeedUp : ItemBase
{
    [SerializeField]
    private AudioClip _moveUpSound;
    protected override void ApplyItemEffect(GameObject player)
    {
        PlayerMove playerMove = player.GetComponent<PlayerMove>();
        playerMove.GetMoveSpeedUp();
    }
    protected override void ApplyItemSound()
    {
        SoundManager.Instance.PlaySFX(_moveUpSound);
    }
}