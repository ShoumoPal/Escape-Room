using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    private void Awake()
    {
        playButton.onClick.AddListener(ResumeGame);
        homeButton.onClick.AddListener(BackToLobby);
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }
    private void BackToLobby()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonPress);
        LevelManager.Instance.Lobby();
    }

    private void ResumeGame()
    {
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonPress);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
