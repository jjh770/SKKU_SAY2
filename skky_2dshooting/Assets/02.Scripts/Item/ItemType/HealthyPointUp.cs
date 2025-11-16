using UnityEngine;

public class HealthyPointUp : ItemBase
{
    [SerializeField] private AudioClip _healthyUpSound;
    [SerializeField] private int _healthyPointValue;

    protected override void ApplyItemEffect(GameObject player)
    {
        Player playerComponent = player.GetComponent<Player>();
        playerComponent.HealthyPointUp(_healthyPointValue);
    }
    protected override void ApplyItemSound()
    {
        SoundManager.Instance.PlaySFX(_healthyUpSound);
    }
}
