using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Chapter8 : MonoBehaviour
{

    public string MBTI = "ESTP";
    int Score = 0;

    void Start()
    {
        switch(Score / 10)
        {
            case 10:
            {
                Debug.Log("A+");
                break;
            }
            case 9:
            {
                Debug.Log("A");
                break;
            }
            case 8:
            {
                Debug.Log("B");
                break;
            }
        }
    }
}
