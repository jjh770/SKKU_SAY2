using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 목표 :적을 죽일 때마다 점수를 올리고, 현재 점수를 UI에 표시한다.

    // 필요 속성
    // - 현재 점수 UI(Text 컴포넌트) (규칙 : UI 요소는 항상 변수명 뒤에 UI를 붙인다.)
    [SerializeField]
    private Text _currentScoreTextUI;
    // - 현재 점수 (int)
    private int _currentScore = 0;

    private void Start()
    {
        Refresh();
    }

    public void AddScore(int score)
    {
        if (score <= 0) return;

        _currentScore += score;
        Refresh();
    }

    private void Refresh()
    {
        _currentScoreTextUI.text = $"현재 점수 : {_currentScore.ToString("0,0")}";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            TestSave();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            TestLoad();
        }
    }

    private void TestSave()
    {
        // 유니티에서 값을 저장할 때 'PlayerPrefs'라는 기능을 제공한다.
        // 저장 가능한 자료형은 int, float, string 3가지이다.
        // 저장할 때는 저장할 이름(key)와 값(value)을 같이 저장한다.
        // 저장 : Set
        // 로드 : Get

        PlayerPrefs.SetInt("score", _currentScore);
        Debug.Log("저장되었습니다.");
    }

    private void TestLoad()
    {
        // 값을 불러올 때는 저장할 때 사용한 이름(key)을 사용한다.
        // 만약 해당 이름으로 저장된 값이 없다면, 기본값(default value)을 반환한다.
        _currentScore = PlayerPrefs.GetInt("score", 0); // 저장된 값이 없으면 0을 반환
        Refresh();
    }
}
