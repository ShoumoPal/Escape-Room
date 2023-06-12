using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private Button restart;
    [SerializeField] private Button home;

    private void Awake()
    {
        restart.onClick.AddListener(RestartLevel);
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
        SceneManager.LoadScene(0);
    }

    private void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
