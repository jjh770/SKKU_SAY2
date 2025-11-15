using UnityEngine;

public class BossShootingState : IBossState
{
    private BossMovement _boss;
    private BossFire _bossFire;

    public BossShootingState(BossMovement boss)
    {
        _boss = boss;
        _bossFire = _boss.GetComponent<BossFire>();
    }

    public void Enter()
    {
        _boss.ShootingMotion(() =>
        {
            _bossFire.IsBulletFire(false);
            _boss.SetState(new BossDefeatState(_boss));
        });
        _bossFire.IsBulletFire(true);
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
