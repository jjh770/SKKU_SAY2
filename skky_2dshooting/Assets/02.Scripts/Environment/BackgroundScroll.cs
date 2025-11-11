using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    // 목표 : 배경 스크롤 구현

    // 필요 속성
    // - 머터리얼
    public Material BackgroundMaterial;
    // - 스크롤 속도
    public float ScrollSpeed = 0.1f;



    void Update()
    {
        // 방향을 구한다.
        Vector2 direction = Vector2.up; 
        // 움직인다. (스크롤)
        BackgroundMaterial.mainTextureOffset += direction * ScrollSpeed * Time.deltaTime;
    }
}
