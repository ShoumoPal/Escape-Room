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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerController>() != null)
        {
            //Level complete
            LevelManager.Instance.SetCurrentLevelComplete();
            SoundManager.Instance.PlaySFX(SoundTypes.LevelCompleteSound);
            glowAnim.SetTrigger("Glow");
            StartCoroutine(LoadNextLevel());

        }
    }
    IEnumerator LoadNextLevel()
    {
        crossfadeAnim.SetTrigger("Start");
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
