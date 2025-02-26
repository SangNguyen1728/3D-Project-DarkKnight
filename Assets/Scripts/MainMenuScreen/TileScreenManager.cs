using UnityEngine;
using Unity.Netcode;
using Unity.Netcode;
using UnityEngine.UI;
public class TileScreenManager : MonoBehaviour
{
    [Header("Menu")]
    [SerializeField] GameObject titleScreenMainMenu;
    [SerializeField] GameObject tileScreenLoadMenu;

    [Header("button")]
    [SerializeField] Button loadMenuReturnButton;
    [SerializeField] Button mainMenuLoadGameButton;

    public void StartNetWorkAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void StartNewGame()
    {
        WorldSaveGameManager.instance.CreateNewGame();
        StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
    }

    public void OpenLoadGameMenu()
    {
        // close main menu
        titleScreenMainMenu.SetActive(false);

        // open load menu
        tileScreenLoadMenu.SetActive(true);

        // select the return button first
        loadMenuReturnButton.Select();
    }
    public void CloseLoadGameMenu()
    {
        // close load menu
        tileScreenLoadMenu.SetActive(false);

        // open main menu
        titleScreenMainMenu.SetActive(true);

        // select the load button 
        mainMenuLoadGameButton.Select();
    }
}
