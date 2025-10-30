using UnityEngine;

public class RecordButtonState : IUIState
{
    private RecordingSystem recordingSystem;
    private Transform playerTransform;

    public RecordButtonState(RecordingSystem system, Transform player)
    {
        recordingSystem = system;
        playerTransform = player;
    }
    public void Enter() => recordingSystem.StartRecording(playerTransform);
    public void Exit() => recordingSystem.StopRecording();
    public string GetButtonText() => "replay";
}