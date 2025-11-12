using System;

[Serializable]
public class UserData
{
    public int CurrentScore;
    public int BestScore;

    // 생성자
    public UserData(int score, int bestScore)
    {
        this.CurrentScore = score;
        this.BestScore = bestScore;
    }
}