using UnityEngine;

public class Chapter5 : MonoBehaviour
{

    void Start()
    {
        // 연산 : 변수들을 이용해서 어떤 결과를 얻는 것, 주어진 식을 계산하여 결과를 얻는 것

        // 연산자 : 연산할 때 필요한 기호

        // 1. 산술 연산자
        // -> 사칙연산 ( + - / % ) 를 수행하는 연산자
        int a = 5 + 6; // 모든 연산의 결과는 왼쪽(변수)에 저장된다.
        Debug.Log(a);

        int b = a - 3;
        Debug.Log(b);

        int c = 7 * b;
        Debug.Log(c);

        // 정수 -> 소수 형변환
        // 정수에서 소수로 암시적 형변환이 일어남.
        // 소수점 아래 숫자들이 사라지는 문제가 일어남.
        // int d = 27 / 4;
        float d = (float)27 / 4f; // (float)으로 명시적 형변환 or 숫자 뒤 f를 붙여 계산해야함. 둘중 하나만 해도 됨.
        Debug.Log(d);

        // 나머지 연산자
        int e = 27 % 4;
        Debug.Log(e);


        // 2. 대입 연산자 (할당 연산자)
        int number = 7; // '=' : 오른쪽의 값을 왼쪽에 대입 (할당)
        number += 3; 
        number *= 2;
        number -= 1; // 산술 대입 연산자 (덧셈 곱셈 뺄셈 등)


        // 3. 증감 연산자;
        int age = 28;
        age++;
        age--;
    }
}
