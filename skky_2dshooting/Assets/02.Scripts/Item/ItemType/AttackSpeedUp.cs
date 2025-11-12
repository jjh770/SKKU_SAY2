using UnityEngine;

public class AttackSpeedUp : ItemBase
{
    [SerializeField]
    private AudioSource _attackUpSound;
    protected override void ApplyItemEffect(GameObject player)
    {
        PlayerFire playerFire = player.GetComponent<PlayerFire>();
        playerFire.GetAttackSpeedUp();
    }
    protected override void ApplyItemSound()
    {
        SoundManager.Instance.PlaySFX(_attackUpSound.clip);
    }
}
