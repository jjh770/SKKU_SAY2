using UnityEngine;

public class BossDefeatState : IBossState
{
    private BossMovement _boss;
    private float _startTime;
    private float _fallTime;

    public BossDefeatState(BossMovement boss, float fallTime)
    {
        _boss = boss;
        _fallTime = fallTime;
    }

    public void Enter()
    {
        _startTime = Time.time;
        Debug.Log("패배");
    }

    public void Update()
    {
        if (Time.time - _startTime > _fallTime)
        {
            _boss.SetState(new BossMoveState(_boss));
        }
        else
        {
            //_boss.SetDirectionToPlayer();
        }
    }

    public void Exit()
    {
        // 상태 전환 시 필요한 처리 (없으면 빈 함수)
    }
}
