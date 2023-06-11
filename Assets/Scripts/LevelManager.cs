using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}

[Serializable]
public class Levels
{
    public string levelName;
}
public class LevelManager : MonoBehaviour
{
    public Levels[] levels;
    private static LevelManager instance;
    public static LevelManager Instance { get { return instance; } }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Play Background Music
        SoundManager.Instance.PlayBG(SoundTypes.BackgroundMusic);
        if (GetLevelStatus(levels[0].levelName) == LevelStatus.Locked)
        {
            SetLevelStatus(levels[0].levelName, LevelStatus.Unlocked);
        }
        for(int i = 1; i < levels.Length; i++)
        {
            SetLevelStatus(levels[i].levelName, LevelStatus.Locked);
        }
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        LevelStatus levelStatus = (LevelStatus) PlayerPrefs.GetInt(levelName);
        return levelStatus;
    }
    public void SetLevelStatus(string levelName, LevelStatus levelStatus)
    {
        PlayerPrefs.SetInt(levelName, (int)levelStatus);
    }
    private static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
    public void SetCurrentLevelComplete()
    {
        SetLevelStatus(SceneManager.GetActiveScene().name, LevelStatus.Completed);

        string nextSceneName = NameFromIndex(SceneManager.GetActiveScene().buildIndex + 1);
        SetLevelStatus(nextSceneName, LevelStatus.Unlocked);
    }
}
