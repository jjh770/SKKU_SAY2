using UnityEngine;

public class AttackSpeedUp : ItemBase
{
    protected override void ApplyItemEffect(GameObject player)
    {
        PlayerFire playerFire = player.GetComponent<PlayerFire>();
        playerFire.GetAttackSpeedUp();
    }
}
