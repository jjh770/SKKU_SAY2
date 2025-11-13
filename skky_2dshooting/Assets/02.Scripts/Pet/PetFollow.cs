using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PetFollow : MonoBehaviour
{
    public Vector3 FollowPosition;
    public Transform ParentTransform;
    public Queue<Vector3> ParentQueue;
    public int FollowDelay;

    private void Awake()
    {
        FollowPosition = ParentTransform.position;
        ParentQueue = new Queue<Vector3>();
    }

    private void Update()
    {
        Watch();
        Follow();
    }

    private void Watch()
    {
        if (!ParentQueue.Contains(ParentTransform.position))
        {
            ParentQueue.Enqueue(ParentTransform.position);
        }

        if (ParentQueue.Count > FollowDelay)
        {
            FollowPosition = ParentQueue.Dequeue();
        }
        else if (ParentQueue.Count < FollowDelay)
        {
            FollowPosition = ParentTransform.position;
        }
    }
    private void Follow()
    {
        transform.position = FollowPosition;
    }
}
