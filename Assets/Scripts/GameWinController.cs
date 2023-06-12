using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        Time.timeScale = 0f;
    }
    private void BackToHome()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
