using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 :적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시한다.

    // 필요 속성
    // - 현재 점수 UI(Text 컴포넌트) (규칙 : UI 요소는 항상 변수명 뒤에 UI를 붙인다.)
    [SerializeField]
    private Text _currentScoreTextUI;
    [SerializeField]
    private Text _bestScoreTextUI;
    // - 현재 점수 (int)
    private int _startScore = 0;
    private int _currentScore = 0;
    private int _bestScore = 0;

    private bool _isPlayerDead = false;

    private const string ScoreKey = "Score";
    private const string BestScoreKey = "BestScore";

    private void Start()
    {
        SetStartScore();
        Load();
        Refresh();
    }

    // 하나의 메서드는 한 가지 일만 잘 하면 된다.
    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScoreTextUI.rectTransform.DOKill(true);
        _currentScoreTextUI.rectTransform.localScale = Vector3.one;
        _currentScoreTextUI.rectTransform.DOPunchScale(Vector3.one * 0.3f, 0.5f, 10, 1);

        _currentScore += score;
        Refresh();
        Save();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
        _bestScoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
    }

    private void Save()
    {
        // 유니티에서 값을 저장할 때 'PlayerPrefs'라는 기능을 제공한다.
        // 저장 가능한 자료형은 int, float, string 3가지이다.
        // 저장할 때는 저장할 이름(key)와 값(value)을 같이 저장한다.
        // 저장 : Set
        // 로드 : Get
        PlayerPrefs.SetInt(ScoreKey, _currentScore);
    }

    public void SaveBestScore()
    {
        if (_currentScore < _bestScore) return;

        _bestScore = _currentScore;
        PlayerPrefs.SetInt(BestScoreKey, _bestScore);
    }

    private void SetStartScore()
    {
        _currentScore = _startScore;
    }

    private void Load()
    {
        // 값을 불러올 때는 저장할 때 사용한 이름(key)을 사용한다.
        // 만약 해당 이름으로 저장된 값이 없다면, 기본값(default value)을 반환한다.
        _bestScore = PlayerPrefs.GetInt(BestScoreKey);
        Refresh();
    }

    public void PlayerDie()
    {
        _isPlayerDead = true;

    }

    public bool CheckPlayerDead()
    {
        return _isPlayerDead;
    }
}
