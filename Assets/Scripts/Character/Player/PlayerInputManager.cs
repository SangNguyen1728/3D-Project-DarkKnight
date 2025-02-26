using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInputManager : MonoBehaviour
{
    public static PlayerInputManager instance;

    public PlayerManager player;

    PlayerControls playerControls;

    [Header("player movement input")]
    [SerializeField] Vector2 movementInput;
    public float verticalInput;
    public float horizontalInput;
    public float moveAmount;

    [Header("camera movement input")]
    [SerializeField] Vector2 cameraInput;
    public float cameraVerticalInput;
    public float cameraHorizontalInput;

    [Header("player action input")]
    [SerializeField] bool dodgeInput = false;
    [SerializeField] bool sprintInput = false;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        // when the scene changes, run this logic
        SceneManager.activeSceneChanged += OnSceneChange;

        instance.enabled = false;
    }
    private void OnSceneChange(Scene oldScene, Scene newScene)
    {
        // if we are loading into our world scene, enable our player controls
        if(newScene.buildIndex == WorldSaveGameManager.instance.GetWorldScenIndex())
        {
            instance.enabled = true;
        }
        // otherwise we must be at the main menu, disable our player controls
        // this is so my player cânt move around if i enter things like charater creation menu
        else
        {
            instance.enabled = false;
        }
    }

    private void OnEnable()
    {
        if(playerControls == null)
        {
            playerControls = new PlayerControls();

            playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            playerControls.PlayerCamera.Movement.performed += i => cameraInput = i.ReadValue<Vector2>();
            playerControls.PlayerAction.Dodge.performed += i => dodgeInput = true;

            // holding the input, set bool to true
            playerControls.PlayerAction.Sprint.performed += i => sprintInput = true;
            // releasing the input, set bool to false
            playerControls.PlayerAction.Sprint.canceled += i => sprintInput = false;
        }

        playerControls.Enable();
    }
    private void OnDestroy()
    {
        // if we destroy this object, unsubscribe form this event
        SceneManager.activeSceneChanged -= OnSceneChange;
    }
    private void OnApplicationFocus(bool focus)
    {
        if(enabled)
        {
            if(focus)
            {
                playerControls.Enable();
            }
            else
            {
                playerControls.Disable();
            }
        }
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void HandleAllInputs()
    {
        HandlePlayerMovementInput();
        HandleCameraMovementInput();
        HandleDodgeInput();
        HandleSprinting();
    }

    // movement
    private void HandlePlayerMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

        // we clamp the value
        if (moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if(moveAmount > 0.5 && moveAmount <=1)
        {
            moveAmount = 1;
        }

        if (player == null)
            return;

        player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount, player.playerNetWorkManager.isSprinting.Value);
    }
    private void HandleCameraMovementInput()
    {
        cameraVerticalInput = cameraInput.y;
        cameraHorizontalInput = cameraInput.x;


    }

    // actions
    private void HandleDodgeInput()
    {
        if(dodgeInput)
        {
            dodgeInput = false;
            // return nothing if menu or ui window is open
            player.playerLocomotionManager.AttemptToPerformDodge();

        }
    }
    private void HandleSprinting()
    {
        if(sprintInput)
        {
            player.playerLocomotionManager.HandleSprinting();
        }
        else
        {
            player.playerNetWorkManager.isSprinting.Value = false;
        }
    }
}
