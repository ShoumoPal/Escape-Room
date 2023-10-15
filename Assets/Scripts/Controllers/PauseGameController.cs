using UnityEngine;
using UnityEngine.UI;

public class PauseGameController : MonoBehaviour
{
    [SerializeField]
    private Button pauseButton;
    [SerializeField]
    private GameObject pauseMenu;

    private void Awake()
    {
        pauseButton.onClick.AddListener(PauseGame);
    }

    private void PauseGame()
    {
        //Show pause Menu

        pauseMenu.SetActive(true);
    }
}
