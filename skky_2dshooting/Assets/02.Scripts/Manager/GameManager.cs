using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float CameraHalfWidth => _cameraHalfWidth;
    public float CameraHalfHeight => _cameraHalfHeight;

    private float _cameraHalfWidth;
    private float _cameraHalfHeight;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Start()
    {
        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

}
