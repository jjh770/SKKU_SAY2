using DG.Tweening;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

// 현재 상태가 뭔지
public class BossMovement : MonoBehaviour
{
    private IBossState _currentState;
    private Vector2 _appearPosition = new Vector2(0, 3.5f);
    private float _appearTime = 3f;

    private Sequence _shootingSequence;
    private Vector2[] _shootingPosition = { new Vector2(-1, 3.5f), new Vector2(1, 3.5f) };
    private float _shootingDuration = 5f;

    public float DefaultSpeed { get; private set; } = 10f;
    public float FallTime { get; private set; } = 3f;

    private void OnEnable()
    {
        Enemy.OnBossDead += BossStageClear;
    }
    private void OnDisable()
    {
        Enemy.OnBossDead -= BossStageClear;
    }
    private void Start()
    {
        SetState(new BossAppearState(this));
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

    public void AppearMotion(Action onAppearComplete)
    {
        transform.DOMove(_appearPosition, _appearTime).SetEase(Ease.InOutExpo)
            .OnComplete(() =>
            {
                onAppearComplete?.Invoke();
            });
    }

    public void ShootingMotion(Action onShootingComplete)
    {
        _shootingSequence = DOTween.Sequence( )
            .Append(transform.DOMove(_shootingPosition[0], _shootingDuration / 2).SetEase(Ease.Linear))
            .Append(transform.DOMove(_shootingPosition[1], _shootingDuration).SetEase(Ease.Linear))
            .Append(transform.DOMove(_appearPosition, _shootingDuration / 2).SetEase(Ease.Linear))
            .SetLoops(-1)
            .OnKill(() =>
            {
                onShootingComplete?.Invoke();
            });
    }

    public void DefeatMotion()
    {
        DOTween.Sequence()
            .Append(transform.DOMove(transform.position - Vector3.down, 3f))
            .Join(GetComponent<SpriteRenderer>().DOFade(0, 3f));
    }

    private void BossStageClear()
    { 
        if(_shootingSequence != null)
        {
            _shootingSequence.Kill();
        }
    }
}
