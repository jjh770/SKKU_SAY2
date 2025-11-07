using UnityEngine;

public class Player : MonoBehaviour
{
    private float _health = 3;

    public void HealthyPointUp(int value)
    {
        _health += value;
    }
    public void Hit(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
