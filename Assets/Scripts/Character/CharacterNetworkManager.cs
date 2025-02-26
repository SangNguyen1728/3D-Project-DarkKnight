using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CharacterNetworkManager : NetworkBehaviour
{
    CharacterManager character;

    [Header("Position")]
    public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<Quaternion> networkRotaion = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public Vector3 networkPositionVelocity;
    public float networkPositionSmoothTime = 0.1f;
    public float networkRotationSmoothTime = 0.1f;

    [Header("Animator")]
    public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Flags")]
    public NetworkVariable<bool> isSprinting = new NetworkVariable<bool>(false, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Stats")]
    public NetworkVariable<int> endurance = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<float> currentStamina = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> maxStamina = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    protected virtual void Awake()
    {
        character = GetComponent<CharacterManager>();
    }
    // Send request from client to server(in case: host): serverrpc
    // Send information from the server(in case: host) to all clients: clientrpc
    // Bandwidth Limits
    // Do not send too much data continuously through ServerRpc to avoid lag.

    // a client rpc í sent to all client present, from the server

    [ServerRpc] //server

    // ulong: unsigned long
    public void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string aniamtionID, bool applyRootMotion)
    {
        // if this character is the host/ server, then activate the client RPC
        if(IsServer)
        {
            PlayerActionAnimationForAllClientsClientRpc(clientID, aniamtionID, applyRootMotion);
        }
    }

    [ClientRpc] // client
    public void PlayerActionAnimationForAllClientsClientRpc(ulong clientID, string aniamtionID, bool applyRootMotion)
    {

        // we make sure to not run the function on the character who sent it (so we dont play aniamtion clip twice)
        if(clientID != NetworkManager.Singleton.LocalClientId)
        {
            PerformActionAanimationFromServer(aniamtionID, applyRootMotion);
        }
    }
    private void PerformActionAanimationFromServer(string aniamtionID, bool applyRootMotion)
    {
        character.applyRootMotion = applyRootMotion;
        character.animator.CrossFade(aniamtionID, 0.2f);
    }
}
