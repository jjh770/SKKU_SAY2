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
        // Ű ���� ó��
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

        // Ű �� ó��
        if (Keyboard.current.leftArrowKey.wasReleasedThisFrame || Keyboard.current.aKey.wasReleasedThisFrame)
        {
            leftPressed = false;
            if (rightPressed)
            {
                OnMoveInput?.Invoke(1f); // ������ Ű ���� ����
                recordingSystem?.RecordInput(InputData.InputType.MoveRight);
            }
            else
            {
                OnMoveInput?.Invoke(0f); // �̵� ����
                recordingSystem?.RecordInput(InputData.InputType.MoveStop);
            }
        }
        if (Keyboard.current.rightArrowKey.wasReleasedThisFrame || Keyboard.current.dKey.wasReleasedThisFrame)
        {
            rightPressed = false;
            if (leftPressed)
            {
                OnMoveInput?.Invoke(-1f); // ���� Ű ���� ����
                recordingSystem?.RecordInput(InputData.InputType.MoveLeft);
            }
            else
            {
                OnMoveInput?.Invoke(0f);
                recordingSystem?.RecordInput(InputData.InputType.MoveStop);
            }
        }

        // Ű�� ��� �������� ��(�������� �ѹ��� ��ġ ��û)
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
