using System.Collections.Generic;
using UnityEngine;

public class RecordingSystem : MonoBehaviour
{
    private List<InputData> recordedInputs = new List<InputData>();
    private bool isRecording = false;
    private float recordStartTime;
    public Vector3 startPosition { get; private set; }

    public bool IsRecording => isRecording;

    public void StartRecording(Transform target)
    {
        isRecording = true;
        recordedInputs.Clear();
        recordStartTime = Time.time;
        startPosition = target.position;
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void RecordInput(InputData.InputType inputType)
    {
        if (!isRecording) return;
        float elapsed = Time.time - recordStartTime;
        recordedInputs.Add(new InputData(elapsed, inputType));
    }

    public List<InputData> GetRecordedData()
    {
        return new List<InputData>(recordedInputs);
    }

    public bool HasRecordedData()
    {
        return recordedInputs.Count > 0;
    } 
}
