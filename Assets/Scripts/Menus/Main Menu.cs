using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene("Level Select Menu");
    }

    public void Settings()
    {
        SceneManager.LoadScene("");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
