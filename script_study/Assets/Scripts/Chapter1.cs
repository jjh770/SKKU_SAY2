// 왜 유니티 "엔진" 이라고 불리는가?
// 프레임워크, 라이브러리를 제외하고도 사운드, 애니메이션 등 다양한 작업을 수행할 수 있기 때문.
using UnityEngine;

public class Chapter1 : MonoBehaviour
{
    void Start()
    {
        // 변수(Variable)
        // 변수 : 데이터를 담는 그릇 / 데이터를 사용하기 위한 이름을 가진 메모리 영역
        // 게임 데이터를 담을 수 있는 그릇 or 상자
        // - 이 변수를 어디서 활용할 것인지?
        // - 어떤 데이터를 담을 것인지?
        // - 이 상자의 이름을 뭐라고 할 것인지?

        // 변수 선언 (변수 생성법)
        // <자료형> <변수이름> = <데이터> 
        // ; 세미콜론은 문장(명령어)의 끝을 의미함.
        int health = 305;
        int Health = 705; // 변수의 이름은 대, 소문자를 구분한다.

        health = 100; // 변수의 값은 수정할 수 있다.

        int damage = 500;
        // float 뒤에 f를 붙이는 이유
        // 82.4를 변수에 넣기 전에 임시로 메모리에 저장하게 되는데
        // 그 때 저장되는 타입이 double이기 때문에 뒤에 f를 붙임
        // long타입 또한 int형으로 임시 저장 되기 때문에 l을 붙여야함
        float weight = 82.4f;

        // 문자열 자료형 string
        string name = "용감한 쿠키"; // 이름을 저장할 변수

        // bool 자료형 (참 true, 거짓 false의 값)
        bool isDie = false; // 생사여부 저장할 변수

        Debug.Log("게임이 시작되었습니다.");
        Debug.Log(health);
        Debug.Log(damage);
        Debug.Log(weight);
        Debug.Log(name);
        Debug.Log(isDie);
    }
}
