using DG.Tweening;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// 현재 상태가 뭔지
public class BossMovement : MonoBehaviour
{
    private IBossState _currentState;
    private Vector2 _appearPosition = new Vector2(0, 3.5f);

    public float DefaultSpeed { get; private set; } = 10f;
    public float AppearTime { get; private set; } = 3f;
    public float FallTime { get; private set; } = 3f;

    private void Start()
    {
        SetState(new BossAppearState(this, AppearTime));
    }

    public void SetState(IBossState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }

    private void Update()
    {
        _currentState.Update();
    }

    public void AppearMotion()
    {
        transform.DOMove(_appearPosition, AppearTime);
    }
}
