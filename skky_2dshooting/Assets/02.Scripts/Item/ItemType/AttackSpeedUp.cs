using UnityEngine;

public class AttackSpeedUp : ItemBase
{
    [SerializeField]
    private AudioClip _attackUpSound;
    protected override void ApplyItemEffect(GameObject player)
    {
        PlayerFire playerFire = player.GetComponent<PlayerFire>();
        playerFire.GetAttackSpeedUp();
    }
    protected override void ApplyItemSound()
    {
        SoundManager.Instance.PlaySFX(_attackUpSound);
    }
}
