using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private RecordReplayButton uiButton;

    private GameObject player;
    private PlayerInput playerInput;
    private PlayerMovement playerMovement;
    private PlayerJump playerJump;
    private RecordingSystem recordingSystem;
    private ReplaySystem replaySystem;

    void Start()
    {
        InitializePlayer();
        InitializeSystems();
        ConnectInputToActions();
    }

    void InitializePlayer()
    {
        player = Instantiate(playerPrefab, new Vector3(0, -3.5f, 0), Quaternion.identity);
        playerInput = player.GetComponent<PlayerInput>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerJump = player.GetComponent<PlayerJump>();
    }

    void InitializeSystems()
    {
        recordingSystem = gameObject.AddComponent<RecordingSystem>();
        replaySystem = gameObject.AddComponent<ReplaySystem>();
        replaySystem.Initialize(playerInput);
        playerInput.SetRecordingSystem(recordingSystem);
        uiButton.Initialize(recordingSystem, replaySystem, player.transform);
    }

    void ConnectInputToActions()
    {
        playerInput.OnMoveInput += direction =>
        {
            Debug.Log($"이동 입력 수신: {direction}");
            playerMovement.Move(direction);
        };

        playerInput.OnJumpInput += () =>
        {
            Debug.Log("점프 입력 수신");
            playerJump.Jump();
        };
    }

    void OnDestroy()
    {
        if (playerInput != null)
        {
            playerInput.OnMoveInput -= playerMovement.Move;
            playerInput.OnJumpInput -= playerJump.Jump;
        }
    }
}
