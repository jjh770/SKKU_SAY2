using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaySystem : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool isReplaying = false;

    public bool IsReplaying => isReplaying;

    public void Initialize(PlayerInput input)
    {
        playerInput = input;
    }

    public void StartReplay(List<InputData> recordedInputs, Vector3 startPosition)
    {
        if (playerInput == null) return;

        // 1. 플레이어 위치와 속도 초기화
        playerInput.transform.position = startPosition;
        var rb = playerInput.GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        StartCoroutine(ReplayRoutine(recordedInputs));
    }

    public void StopReplay()
    {
        isReplaying = false;
        StopAllCoroutines();
        playerInput.SetReplayMode(false);
    }

    private IEnumerator ReplayRoutine(List<InputData> inputs)
    {
        isReplaying = true;
        playerInput.SetReplayMode(true);

        int index = 0;
        float startTime = Time.time;

        while (index < inputs.Count && isReplaying)
        {
            float elapsed = Time.time - startTime;
            InputData inputData = inputs[index];

            if (elapsed >= inputData.timestamp)
            {
                Debug.Log($"리플레이 입력 재생: {inputData.inputType} at {elapsed}");
                playerInput.SimulateInput(inputData.inputType);
                index++;
            }
            yield return null;
        }

        isReplaying = false;
        playerInput.SetReplayMode(false);
        Debug.Log("리플레이 종료");
    }
}
