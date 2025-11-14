using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 매니저는 단 하나여야만 한다
    // 전역적인 접근점을 제공해야함.
    // 게임 개발에서는 보통 Manager(관리자) 클래스를 보통 싱글 톤 패턴으로 사용하는 것이 관행이다.
    // 인스턴스가 단 하나임을 보장하고 접근이 용이하게 하기 위해 싱글톤 패턴을 사용.

    private static ScoreManager _instance;
    public static ScoreManager Instance => _instance;
    //public static ScoreManager Instance = null;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
    }
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

    private int _thisGameScore = 0;
    private bool _isBossSpawned = false;

    private bool _isPlayerDead = false;

    private UserData _userData;
    private const string SaveKey = "UserData";

    private void Start()
    {
        _userData = new UserData(_startScore, _bestScore);

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
        _thisGameScore += score;
        if (_thisGameScore >= 5000 && !_isBossSpawned)
        {
            FindAnyObjectByType<EnemySpawner>().SpawnBoss();
            _isBossSpawned = true;
        }
        Refresh();
        Save();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore:N0}";
        _bestScoreTextUI.text = $"최고 점수 : {_bestScore:N0}";
        Debug.Log($"이번 게임 점수 : {_thisGameScore}");
    }

    private void Save()
    {
        // 유니티에서 값을 저장할 때 'PlayerPrefs'라는 기능을 제공한다.
        // 저장 가능한 자료형은 int, float, string 3가지이다.
        // 저장할 때는 저장할 이름(key)와 값(value)을 같이 저장한다.
        // 저장 : Set
        // 로드 : Get

        // UserData를 JSON 문자열로 변환
        string jsonData = JsonUtility.ToJson(_userData);
        // JSON 문자열을 PlayerPrefs에 저장
        PlayerPrefs.SetString(SaveKey, jsonData);
        PlayerPrefs.Save();

        Debug.Log($"데이터 저장 완료: {jsonData}");
    }

    public void SaveBestScore()
    {
        if (_currentScore < _bestScore) return;

        _bestScore = _currentScore;
        _userData.BestScore = _bestScore;

        Save();
    }

    private void SetStartScore()
    {
        _currentScore = _startScore;
        _userData.CurrentScore = _currentScore;
    }

    private void Load()
    {
        // 값을 불러올 때는 저장할 때 사용한 이름(key)을 사용한다.
        // 만약 해당 이름으로 저장된 값이 없다면, 기본값(default value)을 반환한다.
        // 저장된 데이터가 있는지 확인
        if (PlayerPrefs.HasKey(SaveKey))
        {
            // PlayerPrefs에서 JSON 문자열 로드
            string jsonData = PlayerPrefs.GetString(SaveKey);

            // JSON 문자열을 UserData 객체로 변환
            _userData = JsonUtility.FromJson<UserData>(jsonData);

            // UserData에서 값 복원
            _currentScore = _userData.CurrentScore;
            _bestScore = _userData.BestScore;
        }
        else
        {
            // 저장된 데이터가 없으면 기본값으로 초기화
            _userData = new UserData(0, 0);
            _currentScore = 0;
            _bestScore = 0;
        }
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
