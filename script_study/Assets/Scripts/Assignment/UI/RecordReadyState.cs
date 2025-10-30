using UnityEngine;

public class RecordReadyState : IUIState
{
    private RecordButtonState recordState;

    public RecordReadyState(RecordButtonState record)
    {
        recordState = record;
    }

    public void Enter()
    {
        // 녹화 대기 상태이므로 실제 녹화 시작하지 않음
        Debug.Log("녹화 대기 상태 (녹화 시작 대기중)");
    }

    public void Exit()
    {
        // 대기 상태 종료 시 녹화 시작
        recordState.Enter();
    }

    public string GetButtonText()
    {
        return "녹화 대기";  // 원하는 메시지로 변경 가능 ("녹화 시작" 등)
    }
}
