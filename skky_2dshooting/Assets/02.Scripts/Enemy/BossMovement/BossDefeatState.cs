using UnityEngine;

public class BossDefeatState : IBossState
{
    private BossMovement _boss;

    public BossDefeatState(BossMovement boss)
    {
        _boss = boss;
    }

    public void Enter()
    {
        _boss.DefeatMotion();
    }

    public void Update()
    {

    }

    public void Exit()
    {
        // 상태 전환 시 필요한 처리 (없으면 빈 함수)
    }
}
