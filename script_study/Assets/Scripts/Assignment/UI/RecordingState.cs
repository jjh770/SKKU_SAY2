using UnityEngine;

public class RecordingState : IUIState
{
    private RecordingSystem recordingSystem;
    private Transform playerTransform;

    public RecordingState(RecordingSystem system, Transform player)
    {
        recordingSystem = system;
        playerTransform = player;
    }

    public void Enter()
    {
        Debug.Log("녹화 시작");
        recordingSystem.StartRecording(playerTransform);
    }

    public void Exit()
    {
        Debug.Log("녹화 종료");
        recordingSystem.StopRecording();
    }

    public string GetButtonText()
    {
        return "Replay";
    }
}
