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
        gameObject.SetActive(true);
        shooting.shootingDisabled = true;
        Time.timeScale = 0;
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        shooting.shootingDisabled = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
