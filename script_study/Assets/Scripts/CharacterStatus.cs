using UnityEngine;

public enum EClassType { 
    Anemo,
    Geo,
    Pyro,
    Hydro,
}
public enum EPositionType {
    DamageDealer,
    Supporter,
    Balance,
}
public class CharacterStatus : MonoBehaviour
{
    public string CharacterName;       // 캐릭터 이름 
    public EClassType ClassType;       // 캐릭터 타입
    public EPositionType PositionType; // 포지션 타입
    public int HealthPoint;            // HP
    public int HealthPointBonus;       // 추가 HP
    public int AttackPoint;            // 공격
    public int AttackBonus;            // 추가 공격
    public int DefensePoint;           // 방어
    public int DefenseBonus;           // 추가 방어
    public float CriticalRate;         // 치명타율 
    public float CriticalDamage;       // 치명타 피해 
    public float WeakSpot;             // 약점 포착

    private void Start()
    {
        CharacterName = "치토세";
        ClassType = EClassType.Hydro;
        PositionType = EPositionType.DamageDealer;
        HealthPoint = 54677;
        HealthPointBonus = 4049;
        AttackPoint = 4709;
        AttackBonus = 970;
        DefensePoint = 190;
        DefenseBonus = 20;
        CriticalRate = 5.0f;
        CriticalDamage = 150.0f;
        WeakSpot = 0.0f;
    }
}
