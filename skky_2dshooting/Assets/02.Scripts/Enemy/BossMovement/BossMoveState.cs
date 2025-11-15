using UnityEngine;

public class BossMoveState : IBossState
{
    private BossMovement _boss;
    private float _speed;

    public BossMoveState(BossMovement boss)
    {
        _boss = boss;
        _speed = boss.DefaultSpeed;
    }

    public void Enter()
    {
        // 최초 진입 시 방향 설정
        //_boss.SetDirectionToPlayer();
        Debug.Log("Move");
    }

    public void Update()
    {
        //_boss.MoveAlongDirection(_speed);
    }

    public void Exit()
    {
        // 상태 종료 시 처리
    }
}
