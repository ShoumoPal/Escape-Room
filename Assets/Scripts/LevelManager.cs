using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelStatus
{
    Locked,
    Unlocked,
    Completed
}
public class Levels
{
    public static string Level1 = "LevelOne";
    public static string Level2 = "LevelTwo";
}
public class LevelManager : MonoBehaviour
{
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
        if(GetLevelStatus(Levels.Level1) == LevelStatus.Locked)
        {
            SetLevelStatus(Levels.Level1, LevelStatus.Unlocked);
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
}
