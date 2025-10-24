using UnityEngine;

public class Chapter3 : MonoBehaviour
{
    // 클래스 멤버 변수 (프로퍼티)
    // 클래스 하위에 있는 모든 함수들이 접근 가능하다.
    // const는 상수 키워드
    // 변수를 선언할 때만 초기값 설정이 가능하고, 그 이후에는 값 변경 불가
    /**const**/ int age = 29;

    float height = 186.5f;

    void Start()
    {
        // 지역 변수 : 함수 내부에서 선언하는 변수, 함수 내부에서만 사용할 수 있다.
        int health = 100;
        Debug.Log(health);

        age = 28;
        Debug.Log(age);
    }
    private void Update()
    {
        Debug.Log(age);

        // 쉐도잉 : 지역변수와 멤버변수의 이름이 같아서 가려지는 현상
        // 참고 : https://titathecheese.tistory.com/35
        float height = 200.1f;

        height = 190.1f;
        Debug.Log(height);
    }
}
