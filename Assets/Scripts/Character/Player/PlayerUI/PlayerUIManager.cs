using UnityEngine;
using Unity.Netcode;
public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;

    [Header("NETWORK JOINT")]
    [SerializeField] bool startGameAsClient;

    [HideInInspector] public PlayerHudManager playerHudManager;


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

        playerHudManager = GetComponentInChildren<PlayerHudManager>();
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if(startGameAsClient)
        {
            startGameAsClient = false;
            // we must first shut down, because we have started as a host during the tile screen    
            NetworkManager.Singleton.Shutdown();
            // we must first shut down, because we have started as a host during the tile screen    
            NetworkManager.Singleton.StartClient();
        }
    }
}
