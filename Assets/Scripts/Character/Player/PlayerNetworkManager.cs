using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerNetworkManager : CharacterNetworkManager
{
    public NetworkVariable<FixedString64Bytes> characterName =
        new NetworkVariable<FixedString64Bytes>("character",
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
}
