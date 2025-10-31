using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RecordReplayButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    private IUIState currentState;
    private IdleState idleState;
    private RecordingState recordingState;
    private ReplayingState replayingState;

    private RecordingSystem recordingSystem;
    private ReplaySystem replaySystem;

    public void Initialize(RecordingSystem recording, ReplaySystem replay, Transform player)
    {
        recordingSystem = recording;
        replaySystem = replay;

        // 4개 상태 객체 생성
        idleState = new IdleState();
        recordingState = new RecordingState(recording, player);
        replayingState = new ReplayingState(replay, recording, this);

        // 초기 상태 설정
        currentState = idleState;
        currentState.Enter();
        UpdateButtonText();

        // 버튼 클릭 이벤트 연결
        button.onClick.AddListener(OnButtonClick);

        // 리플레이 완료 후 자동으로 초기 상태로 전환하기 위한 모니터링
        StartCoroutine(MonitorReplayCompletion());
    }

    private void OnButtonClick()
    {
        if (currentState is IdleState)
        {
            TransitionTo(recordingState);
        }
        else if (currentState is RecordingState)
        {
            TransitionTo(replayingState);
        }
        else if (currentState is ReplayingState)
        {
            TransitionTo(idleState);
        }
    }

    private void TransitionTo(IUIState nextState)
    {
        currentState.Exit();
        currentState = nextState;
        currentState.Enter();
        UpdateButtonText();
    }

    private void UpdateButtonText()
    {
        buttonText.text = currentState.GetButtonText();
    }

    // 리플레이 완료 자동 감지 코루틴
    private IEnumerator MonitorReplayCompletion()
    {
        while (true)
        {
            if (currentState is ReplayingState && !replaySystem.IsReplaying)
            {
                TransitionTo(idleState);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
