using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    [SerializeField]
    private Animator crossfadeAnim;
    [SerializeField]
    private Animator glowAnim;
    [SerializeField]
    private GameObject gameWinPanel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            //Level complete
            LevelManager.Instance.SetCurrentLevelComplete();
            if (LevelManager.Instance.PlayerHasWon())
            {
                gameWinPanel.SetActive(true);
                SoundManager.Instance.PlaySFX(SoundTypes.LevelCompleteSound);
                glowAnim.SetTrigger("Glow");
            }
            else
            {
                SoundManager.Instance.PlaySFX(SoundTypes.LevelCompleteSound);
                glowAnim.SetTrigger("Glow");
                StartCoroutine(LoadNextLevel());
            }
        }
    }
    IEnumerator LoadNextLevel()
    {
        crossfadeAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
