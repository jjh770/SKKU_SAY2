using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public event Action<float> OnMoveInput;
    public event Action OnJumpInput;

    private RecordingSystem recordingSystem;
    private bool isReplayMode = false;

    private bool leftPressed = false;
    private bool rightPressed = false;

    public void SetRecordingSystem(RecordingSystem system)
    {
        recordingSystem = system;
    }
    public void SetReplayMode(bool enabled)
    {
        isReplayMode = enabled;
    }

    void Update()
    {
        if (isReplayMode) return;

        HandleMovementInput();
        HandleJumpInput();
    }

    private void HandleMovementInput()
    {
        // 키 누름 처리
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.aKey.wasPressedThisFrame)
        {
            leftPressed = true;
            OnMoveInput?.Invoke(-1f);
            recordingSystem?.RecordInput(InputData.InputType.MoveLeft);
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame)
        {
            rightPressed = true;
            OnMoveInput?.Invoke(1f);
            recordingSystem?.RecordInput(InputData.InputType.MoveRight);
        }

        // 키 뗌 처리
        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame || Keyboard.current.aKey.wasReleasedThisFrame)
        {
            leftPressed = false;
            if (rightPressed)
            {
                OnMoveInput?.Invoke(1f); // 오른쪽 키 눌림 유지
                recordingSystem?.RecordInput(InputData.InputType.MoveRight);
            }
            else
            {
                OnMoveInput?.Invoke(0f); // 이동 멈춤
                recordingSystem?.RecordInput(InputData.InputType.MoveStop);
            }
        }
        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame || Keyboard.current.dKey.wasReleasedThisFrame)
        {
            rightPressed = false;
            if (leftPressed)
            {
                OnMoveInput?.Invoke(-1f); // 왼쪽 키 눌림 유지
                recordingSystem?.RecordInput(InputData.InputType.MoveLeft);
            }
            else
            {
                OnMoveInput?.Invoke(0f);
                recordingSystem?.RecordInput(InputData.InputType.MoveStop);
            }
        }

        // 키가 계속 눌려있을 때(마지막에 한번만 위치 요청)
        if (leftPressed && !rightPressed)
        {
            OnMoveInput?.Invoke(-1f);
        }
        else if (!leftPressed && rightPressed)
        {
            OnMoveInput?.Invoke(1f);
        }
        else if (!leftPressed && !rightPressed)
        {
            OnMoveInput?.Invoke(0f);
        }
    }

    private void HandleJumpInput()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnJumpInput?.Invoke();
            recordingSystem?.RecordInput(InputData.InputType.Jump);
        }
    }

    public void SimulateInput(InputData.InputType inputType)
    {
        switch (inputType)
        {
            case InputData.InputType.MoveLeft:
                OnMoveInput?.Invoke(-1f);
                break;
            case InputData.InputType.MoveRight:
                OnMoveInput?.Invoke(1f);
                break;
            case InputData.InputType.MoveStop:
                OnMoveInput?.Invoke(0f);
                break;
            case InputData.InputType.Jump:
                OnJumpInput?.Invoke();
                break;
        }
    }
}
