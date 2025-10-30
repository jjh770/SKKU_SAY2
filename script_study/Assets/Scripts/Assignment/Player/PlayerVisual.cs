// PlayerVisual.cs (�׸� �ϳ��� �ִ� �ڵ�)
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [Header("�׸� ����")]
    [SerializeField] private Color boxColor = Color.white;

    void Start()
    {
        // �׸�(ť��) �ϳ��� ����, �ݶ��̴��� ��ġ/ũ��� ���߱�
        GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
        box.name = "PlayerBox";
        box.transform.SetParent(transform);
        box.transform.localPosition = Vector3.zero;
        box.transform.localScale = new Vector3(1f, 2f, 1f);
        box.GetComponent<Renderer>().material.color = boxColor;

        // Renderer �� ������ ������Ʈ�� ����
        Destroy(box.GetComponent<Collider>());
    }
}
