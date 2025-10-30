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
        // ��ȭ ��� �����̹Ƿ� ���� ��ȭ �������� ����
        Debug.Log("��ȭ ��� ���� (��ȭ ���� �����)");
    }

    public void Exit()
    {
        // ��� ���� ���� �� ��ȭ ����
        recordState.Enter();
    }

    public string GetButtonText()
    {
        return "��ȭ ���";  // ���ϴ� �޽����� ���� ���� ("��ȭ ����" ��)
    }
}
