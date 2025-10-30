using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordReplayButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    private IUIState currentState;
    private RecordButtonState recordState;
    private ReplayButtonState replayState;

    public void Initialize(RecordingSystem recording, ReplaySystem replay, Transform player)
    {
        recordState = new RecordButtonState(recording, player);
        replayState = new ReplayButtonState(replay, recording);
        currentState = recordState;
        UpdateButtonText();
        button.onClick.AddListener(OnButtonClick);
    }
    private void OnButtonClick()
    {
        currentState.Exit();
        if (currentState is RecordButtonState) currentState = replayState;
        else currentState = recordState;
        currentState.Enter();
        UpdateButtonText();
    }
    private void UpdateButtonText() => buttonText.text = currentState.GetButtonText();
}
