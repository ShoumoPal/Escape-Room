using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void BackToLobby()
    {
        Time.timeScale = 1f;
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonPress);
        SceneManager.LoadScene(0);
    }

    private void ResumeGame()
    {
        SoundManager.Instance.PlaySFX(SoundTypes.ButtonPress);
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
}
