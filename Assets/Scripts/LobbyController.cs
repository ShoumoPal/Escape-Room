using System;
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
    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        playButton.onClick.AddListener(ShowLevelSelection);
        backButton.onClick.AddListener(BackToLobby);
    }

    private void BackToLobby()
    {
        levelSelectionMenu.SetActive(false);
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
