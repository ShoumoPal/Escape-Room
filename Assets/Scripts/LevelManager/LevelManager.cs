using System;
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
    public Vector3 spawnLocation;
}

public class LevelManager : GenericMonoSingleton<LevelManager>
{
    public Levels[] levels;
    private bool gameWin = false;

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

    public Vector3 GetLocationFromCurrentLevel()
    {
        Levels level = Array.Find(levels, i => i.levelName == SceneManager.GetActiveScene().name);
        return level.spawnLocation;
    }

    public bool PlayerHasWon()
    {
        return gameWin;
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

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex > levels.Length)
        {
            gameWin = true;
        }
        else
        {
            string nextSceneName = NameFromIndex(nextSceneIndex);
            SetLevelStatus(nextSceneName, LevelStatus.Unlocked);
        }
    }
}
