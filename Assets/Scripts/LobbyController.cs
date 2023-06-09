using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button quitButton;
    [SerializeField]
    private GameObject levelSelectionMenu;

    private void Awake()
    {
        playButton.onClick.AddListener(ShowLevelSelection);
    }

    public void ShowLevelSelection()
    {
        levelSelectionMenu.SetActive(true);
    }
}
