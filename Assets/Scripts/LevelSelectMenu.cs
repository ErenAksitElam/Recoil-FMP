using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectMenu : MonoBehaviour
{
    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void TutorialwoWalls()
    {
        SceneManager.LoadScene("TutorialLevel Without Walls");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
}
