using UnityEngine;

public class Chapter2 : MonoBehaviour
{
    void Start()
    {
        
        // 변수가 저장하는 값(데이터)에 따라 타입(자료형)이 다르다.
        
        // 1. 숫자

        // 1-1. 정수형 (소수점이 없는 숫자) : int long short byte
        // 같은 자료형이여도 종류가 여러개인 이유는 담을 수 있는 데이터의 크기가 다르기 때문.
        int age = 29;
        int health = 100;

        // 1-2. 실수형 (소숫점이 없는 숫자) float double decimal 
        // 같은 자료형이더라도 범위 뿐 아니라 '정밀도'가 다르다.
        float weight = 82.4f;
        double height = 186.5;
        weight = 82.4151351564512214132412341f;
        height = 186.513243125312541324134532;
        Debug.Log("몸무게 " + weight + " 키 " + height);
        // 출력값
        // 몸무게 82.41514 키 186.513243125313
        // float 정밀도 : 7자리
        // double 정밀도 : 15자리
        // 정밀도란?
        // 데이터를 표현할 때 소수점 이하 자릿수 등의 오차 없이 보장할 수 있는 정확한 범위

        // 2. 문자열 (문자들, 문장)
        string name = "정종혁";
        name = "정종혁1";
        print(name);
        // Debug.Log()는 Unity에서 사용하는 표준 로그 출력 함수이며,
        // print()`는 Debug.Log()의 별칭(alias)으로,
        // MonoBehaviour에서 상속받은 클래스 내에서 더 간편하게 사용하기 위해 존재합니다.
        // 핵심적인 기능적 차이는 없지만, print()는 클래스가 MonoBehaviour를 상속받아야만 작동하고,
        // Debug.Log()는 MonoBehaviour를 상속받지 않아도 사용 가능합니다. 

        // 3. 논리형

        // 4. 사용자 정의형
    }
}
