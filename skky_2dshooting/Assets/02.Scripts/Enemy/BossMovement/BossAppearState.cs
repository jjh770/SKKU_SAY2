using UnityEngine;

// 상태에 따른 구체적인 행동
public class BossAppearState : IBossState
{
    private BossMovement _boss;
    private float _startTime;

    public BossAppearState(BossMovement boss)
    {
        _boss = boss;
    }
    // 상태 진입할 때 딱 한번 Start랑 비슷
    public void Enter()
    {
        _startTime = Time.time;
        _boss.AppearMotion(() =>
        {
            _boss.SetState(new BossShootingState(_boss));
        });
        Debug.Log("Appear");
    }
    // 말그대로 Update
    public void Update()
    {

    }
    // 상태 나갈 때 딱 한번
    public void Exit()
    {
        // 상태 전환 시 필요한 처리 (없으면 빈 함수)
    }
}
