using UnityEngine;
public class ReplayButtonState : IUIState
{
    private ReplaySystem replaySystem;
    private RecordingSystem recordingSystem;

    public ReplayButtonState(ReplaySystem replay, RecordingSystem recording)
    {
        replaySystem = replay;
        recordingSystem = recording;
    }
    public void Enter()
    {
        replaySystem.StartReplay(
            recordingSystem.GetRecordedData(),
            recordingSystem.startPosition);
    }
    public void Exit() => replaySystem.StopReplay();
    public string GetButtonText() => "record";
}