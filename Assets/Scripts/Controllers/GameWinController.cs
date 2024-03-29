using UnityEngine;
using UnityEngine.UI;

public class GameWinController : MonoBehaviour
{
    [SerializeField] private Button home;

    private void Awake()
    {
        home.onClick.AddListener(BackToHome);
    }
    private void OnEnable()
    {
        SoundManager.Instance.StopFootsteps();
        Time.timeScale = 0f;
    }
    private void BackToHome()
    {
        Time.timeScale = 1f;
        LevelManager.Instance.Lobby();
    }
}
