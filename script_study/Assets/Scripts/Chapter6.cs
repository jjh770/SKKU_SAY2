using UnityEngine;

public class Chapter6 : MonoBehaviour
{
    void Start()
    {
        // 4. 비교(관계) 연산자
        // 양쪽 값을 비교하여 결과를 참 (true) 거짓 (false)으로 판명해주는 연산자
        // 비교 : 값이 큰지 작은지 같은지 다른지
        bool result = 7 > 3;
        Debug.Log(result);

        bool result2 = 10 >= 10;
        Debug.Log(result2);

        int number1 = 23;
        int number2 = 27;
        bool result3 = number1 == number2; // 둘이 같나?
        bool result4 = number1 != number2; // 둘이 다른가?

        // 비교 연산자의 결과는 true / false

        // 5. 논리 연산자
        // 두 개의 true/false를 연산하여 true/false 결과를 얻는 연산자

        // && : AND 연산자  -> 모두 true면 true
        // || : OR 연산자 -> 둘 중 하나만 true면 true
        // !  : NOT 연산자 -> true는 false, false는 true


        // 괄호 연산자 : 연산의 우선순위를 변경
        int numberResult = 3 + 7 * 10;
        Debug.Log(numberResult);
    }
}
