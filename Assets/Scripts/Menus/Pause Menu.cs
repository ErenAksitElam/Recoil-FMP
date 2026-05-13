using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject timer;
    public GameObject healthBar;
    public GameObject primaryIndicator;

    public Shooting shooting;

    public void Pause()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        shooting.shootingDisabled = true;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        shooting.shootingDisabled = false;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
