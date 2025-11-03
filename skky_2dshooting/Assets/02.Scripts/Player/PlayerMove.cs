using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 목표
    // 키보드 입력에 따라 방향을 구하고 그 방향으로 이동시키고 싶다.

    // 1. 키보드 입력
    // 2. 방향 구하는 방법
    // 3. 이동

    [SerializeField]
    private float Speed = 3f;
    [SerializeField]
    private float minSpeed = 1f;
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private float speedChangeAmount = 0.5f;

    private float _cameraHalfWidth;
    private float _cameraHalfHeight;

    // 게임 오브젝트가 생성될 때 (단 한번)
    private void Start()
    {
        _cameraHalfHeight = Camera.main.orthographicSize;
        _cameraHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        transform.position = Vector3.zero;
    }


    // 게임 오브젝트가 게임을 시작한 후 최대한 많이 실행 (지속적으로)
    private void Update()
    {
        MovePlayer();
        HandleMoveSpeed();
    }

    private void HandleMoveSpeed()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Speed += speedChangeAmount;
            if (Speed > maxSpeed)
                Speed = maxSpeed;
            Debug.Log($"Speed increased to: {Speed}");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Speed -= speedChangeAmount;
            if (Speed < minSpeed)
                Speed = minSpeed;
            Debug.Log($"Speed decreased to: {Speed}");
        }
    }

    private void MovePlayer()
    {
        // 1. 키보드 입력을 감지한다.
        // 유니티에서는 Input이라는 모듈이 입력에 관한 모든 것을 담당함.
        // GetAxis -> 원하는 축을 -1 ~ 1 로 가져온다. (입력 했냐 안했냐 판별)
        // GetAxisRaw -> 원하는 축을 -1, 0, 1 로 바로 가져옴 (부드러운 변화 없음)
        float h = Input.GetAxis("Horizontal"); // 수평 입력에 대한 값을 -1 ~ 1 로 가져옴
        float v = Input.GetAxis("Vertical");   // 수직 입력에 대한 값을 -1 ~ 1 로 가져옴

        Debug.Log($"{h} {v}");

        // 2. 입력으로부터 방향을 구한다.
        // Vector : 크기와 방향을 표현하는 물리 개념
        Vector2 direction = new Vector2(h, v);
        // 2-1. 방향을 크기 1로 만드는 정규화를 함. (대각선 이동 시 속도 보정을 위해 정규화하는 것이 좋음)
        direction.Normalize();
        Debug.Log($"{direction.x} {direction.y}");

        // 3. Vector 방향으로 이동한다.
        Vector2 position = transform.position; // 현재 위치 (Vector3로 해도 되지만 z축은 사용하지 않기 때문에 Vector2)

        // 새로운 위치 = 현재 위치 + (방향 * 속력) * 시간
        // 새로운 위치 = 현재 위치 + (속도)        * 시간
        //      새로운 위치 = 현재 위치 + (방향)   *  속도      * 시간
        Vector2 newPosition = position + direction * Speed * Time.deltaTime;  // 새로운 위치

        // 화면 밖으로 나가려는 시도 감지 및 막기
        //Debug.Log("Attempted to move outside screen bounds.");
        //newPosition.x = Mathf.Clamp(newPosition.x, -_cameraHalfWidth, _cameraHalfWidth);
        //newPosition.y = Mathf.Clamp(newPosition.y, -_cameraHalfHeight * (0.5f), _cameraHalfHeight);

        // 화면 밖으로 나가면 반대쪽에서 나오기
        if (newPosition.x > _cameraHalfWidth)
        {
            newPosition.x = -_cameraHalfWidth;
        }
        else if (newPosition.x < -_cameraHalfWidth)
        {
            newPosition.x = _cameraHalfWidth;
        }

        if (newPosition.y > 0)
        {
            newPosition.y = -_cameraHalfHeight ;
        }
        else if (newPosition.y < -_cameraHalfHeight)
        {
            newPosition.y = 0f;
        }

        // Time.deltaTime : 이전 프레임으로부터 현재 프레임까지 시간이 얼마나 흘렀는지 나타내는 값 (delta : 얼마나 변했는가)
        // 각 PC 사양에 따라 다른 FPS 값의 차이를 메꿔줄 수 있음.
        // 1초 / FPS 의 값과 비슷함.

        // 이동속도 : 10
        // PC1 :  50FPS : Update -> 초당  50번 실행 -> 10 *  50 =  500 * (Time.deltaTime)
        // PC2 : 100FPS : Update -> 초당 100번 실행 -> 10 * 100 = 1000 * (Time.deltaTime) // PC1, PC2 두 값이 같아짐

        transform.position = newPosition;      // 새로운 위치로 갱신
    }
}
