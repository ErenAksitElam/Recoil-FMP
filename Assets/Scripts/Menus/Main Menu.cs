using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public void Play()
    {
        SceneManager.LoadScene("Level Select Menu");
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
