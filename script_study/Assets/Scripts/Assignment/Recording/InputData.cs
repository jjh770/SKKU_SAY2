using UnityEngine;

[System.Serializable]
public class InputData
{
    public enum InputType
    {
        MoveLeft,
        MoveRight,
        MoveStop,
        Jump
    }

    public float timestamp;
    public InputType inputType;

    public InputData(float time, InputType type)
    {
        timestamp = time;
        inputType = type;
    }
}
