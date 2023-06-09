using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LevelController : MonoBehaviour
{
    private Button levelButton;
    public string levelName;

    private void Awake()
    {
        levelButton = GetComponent<Button>();
        levelButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch(levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Level locked..");
                break;
            case LevelStatus.Unlocked:
                Debug.Log("Level unlocked..");
                SceneManager.LoadScene(levelName);
                break;
            case LevelStatus.Completed:
                Debug.Log("Level completed..");
                SceneManager.LoadScene(levelName);
                break;

        }
    }
}
