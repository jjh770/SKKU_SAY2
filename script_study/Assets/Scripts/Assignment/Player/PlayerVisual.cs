// PlayerVisual.cs (네모 하나만 있는 코드)
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("네모 색상")]
    [SerializeField] private Color boxColor = Color.white;

    void Start()
    {
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.name = "PlayerBox";
        box.transform.SetParent(transform);
        box.transform.localPosition = Vector3.zero;
        box.transform.localScale = new Vector3(1f, 1f, 1f);
        box.GetComponent<Renderer>().material.color = boxColor;

        Destroy(box.GetComponent<Collider>());
    }
}
