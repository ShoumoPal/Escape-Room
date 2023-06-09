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

    public void PlayButtonHoverSound()
    {
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonHover);
    }
    public void PlayButtonClickSound()
    {
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonPress);
    }
}
