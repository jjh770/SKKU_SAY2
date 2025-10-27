using UnityEngine;

public class Chapter4 : MonoBehaviour
{
    void Start()
    {

        /* 데이터의 형변환이란?
        -> (변수에 담겨 있는) 데이터를 다른 데이터 타입의 변수에 옮겨 담는 것.
        - 크기(값의 범위)가 서로 다른 숫자 사이의 변환
        - 부호 있는 숫자와 부호 없는 숫자 사이의 변환
        - 실수와 정수 사이의 변환
        - 문자(열)과 숫자 사이의 변환 */

        int money = 1700000000; 
        // money = 3700000000; <- int의 값은 -21억 ~ 21억 사이의 값을 포함해야하기 때문에 오류

        // 암시적 형변환
        // 작은 범위의 자료형에서 큰 범위의 자료형으로 변환할때는 자동으로 수행됨.
        long myMoney = money;
        myMoney = 3700000000;

        // 명시적 형변환
        // 더 작은 범위로 변환할때는 형변환을 '명확'하게 표현해야한다.
        short yourMoney = (short)money;
        // 나머지 16억은 어디로 갔을까?
        // 오버플로우, 언더플로우 학습하기


        // 엑셀/메모장/JSON/DB 같은 곳에 데이터를 문자(열)로 표기하는 경우가 있음
        // 숫자->문자, 문자->숫자 로 변환하는 경우는 좀 더 엄격함.

        // 문자열 -> 숫자 (언박싱)
        string age = "17";
        // int myAge = (int)age; <- 문자를 숫자로 변환하는 과정이므로 에러
        // 문자 (참조형 Heap 메모리 영역) 를 숫자 (값형 Stack 메모리 영역)으로 변환하는 과정
        // 언박싱, 반대는 박싱 <- 과제로 정리하기
        int myAge = int.Parse(age); 
        print(myAge);

        // 숫자 -> 문자열 (박싱)
        // ToString() 메서드를 사용한다.
        int height = 186;
        string height2 = height.ToString();
        print(height2);

    }
}
