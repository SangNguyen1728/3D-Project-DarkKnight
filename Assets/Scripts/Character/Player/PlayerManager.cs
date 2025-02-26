using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerManager : CharacterManager
{
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerNetworkManager playerNetWorkManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;
    
    protected override void Awake()
    {
        base.Awake();

        // only for the player

        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerNetWorkManager = GetComponent<PlayerNetworkManager>();
        playerStatsManager = GetComponent<PlayerStatsManager>();
    }
    protected override void Update()
    {
        base.Update();

        // if do not own this gameobject, can not control or edit it
        if (!IsOwner)
            return;

        //Handle Movement
        playerLocomotionManager.HandleAllMovement();

        // regen stamina
        playerStatsManager.RegenerateStamina();
    }
    protected override void LateUpdate()
    {
        if (!IsOwner) 
            return;

        base.LateUpdate();

        PlayerCamera.instance.HandleAllCameraActions();
    }
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        // if this is the player object owned by this client
        if (IsOwner)
        {
            PlayerCamera.instance.player = this;
            PlayerInputManager.instance.player = this;
            
            playerNetWorkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerHudManager.SetNewStaminaValue;
            playerNetWorkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;
           
            // this willl be moved when saving and loading is added
            playerNetWorkManager.maxStamina.Value = playerStatsManager.CalculateStaminaBaseOnEnduranceLevel(playerNetWorkManager.endurance.Value);
            playerNetWorkManager.currentStamina.Value = playerStatsManager.CalculateStaminaBaseOnEnduranceLevel(playerNetWorkManager.endurance.Value);

            PlayerUIManager.instance.playerHudManager.SetMaxStaminaValue(playerNetWorkManager.maxStamina.Value);
        }
    }
    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        currentCharacterData.CharacterName = playerNetWorkManager.characterName.Value.ToString();

        currentCharacterData.xPosition = transform.position.x;
        currentCharacterData.yPosition = transform.position.y;
        currentCharacterData.zPosition = transform.position.z;
    }
    public void LoadGameFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        playerNetWorkManager.characterName.Value = currentCharacterData.CharacterName;
        Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
        transform.position = myPosition;   
    }

}
