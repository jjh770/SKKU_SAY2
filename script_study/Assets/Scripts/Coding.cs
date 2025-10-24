// 나 유니티 엔진 사용할래 선언
using UnityEngine;

// 하나의 스크립트 파일은 하나의 클래스와 대응한다.
// 스크립트를 만들면 자동으로 파일 이름과 클래스 이름이 같다.
// 이름이 같지 않다면 유니티가 인식할 수 없음.
public class Coding : MonoBehaviour // MonoBehaviour : 이 코드 파일을 유니티 컴포넌트로 만들어주는 역할.
{
    // 게임 오브젝트가 게임을 시작할 때 한 번 읽는 영역
    void Start()
    {
        // 디버그 (콘솔) 이라는 창이 있음
        // "~~~~" 를 콘솔 창에 띄워줘
        Debug.Log("게임을 시작합니다.");
    }
    // 게임 오브젝트가 게임을 시작하고나서 계속 읽는 영역
    void Update()
    {
        Debug.Log("게임을 업데이트 합니다.");
    }
}

// 연동 안될 때 참고 : https://bullie.tistory.com/30
  