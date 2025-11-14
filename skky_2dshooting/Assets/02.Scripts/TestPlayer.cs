using UnityEngine;

public class TestPlayer : MonoBehaviour
{
    // Getter / Setter

    // 무결성 : 속성(데이터)의 정확성, 일관성, 유효성
    // 무결성을 위해 Getter, Setter를 구현
    // Setter는 변수에 대한 규칙이 필요함 (규칙이 없으면 쓸 필요가 없음)
    // 즉 외부에서 바꿀 일이 없다면 setter는 필요가 없음
    private int _health; // 0 ~ MaxHealth
    public int Health => _health; // get 프로퍼티

    // 체력이 바뀌는 경우 : 맞았을 때 or 힐
    public void Heal(int amount)
    {
        _health += amount;
    }
    public void Hit(int damage)
    {
        _health -= damage;
    }

    public void Revive()
    {
        // _health = MaxHealth;
    }
}
