using UnityEngine;
using System.Collections;

public class ReplayingState : IUIState
{
    private ReplaySystem replaySystem;
    private RecordingSystem recordingSystem;
    private MonoBehaviour monoBehaviour; // 코루틴 실행용

    public ReplayingState(ReplaySystem replay, RecordingSystem recording, MonoBehaviour mono)
    {
        replaySystem = replay;
        recordingSystem = recording;
        monoBehaviour = mono;
    }

    public void Enter()
    {
        Debug.Log("리플레이 시작");
        replaySystem.StartReplay(
            recordingSystem.GetRecordedData(),
            recordingSystem.startPosition);
    }

    public void Exit()
    {
        Debug.Log("리플레이 종료");
        replaySystem.StopReplay();
    }

    public string GetButtonText()
    {
        return "Replaying";
    }
}
