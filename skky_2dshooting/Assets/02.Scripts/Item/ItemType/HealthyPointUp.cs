using UnityEngine;

public class HealthyPointUp : ItemBase
{
    private int _healthyPointValue;

    protected override void ApplyItemEffect(GameObject player)
    {
        Player playerComponent = player.GetComponent<Player>();
        playerComponent.HealthyPointUp(_healthyPointValue);
    }
}
