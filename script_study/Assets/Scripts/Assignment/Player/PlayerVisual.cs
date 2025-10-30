// PlayerVisual.cs (네모 하나만 있는 코드)
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("네모 색상")]
    [SerializeField] private Color boxColor = Color.white;

    void Start()
    {
        // 네모(큐브) 하나만 생성, 콜라이더의 위치/크기와 맞추기
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.name = "PlayerBox";
        box.transform.SetParent(transform);
        box.transform.localPosition = Vector3.zero;
        box.transform.localScale = new Vector3(1f, 2f, 1f);
        box.GetComponent<Renderer>().material.color = boxColor;

        // Renderer 외 나머지 컴포넌트만 유지
        Destroy(box.GetComponent<Collider>());
    }
}
