using UnityEngine;

// 제어문
// 지금까지 배운 코드는 위에서 아래로 실행되는 흐름을 가지고 있음 -> 순차적
// 순차적인 흐름에 변화(제어)를 주는 것이 '제어문' -> 코드의 흐름을 제어함.
// 제어문의 종류
// - 1. 조건문
// - 2. 분기문
// - 3. 반복문

public class Chapter7 : MonoBehaviour
{
    void Start()
    {
        int health = 100;

        int damage = 90;

        health -= damage;
        Debug.Log("남은 체력" + health);

        // 1. 조건문
        // 조건에 따라 코드 뭉치를 실행할지 말지 정하는 문법
        // 조건 : 비교/논리 연산에 대해 True/False

        // 조건 : 만약 체력이 0보다 작거나 같다면
        bool isUnderZero = health <= 0;
        if (isUnderZero)
        {
            // 조건이 true일 때만 아래 중괋호 범위의 코드가 실행됨.
            Debug.Log("게임오버" + health);
        }
        else if (health > 50)
        {
            Debug.Log("아직 튼튼함");
        }
        else
        {
            Debug.Log("생존");
        }
    }
}
