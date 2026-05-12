using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject timer;
    public GameObject healthBar;
    public GameObject primaryIndicator;

    public void Pause()
    {
        gameObject.SetActive(true);
        timer.SetActive(false);
        healthBar.SetActive(false);
        primaryIndicator.SetActive(false);
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
        timer.SetActive(true);
        healthBar.SetActive(true);
        primaryIndicator.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}
