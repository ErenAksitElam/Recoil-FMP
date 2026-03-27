using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("Level Select Menu");
    }
}
