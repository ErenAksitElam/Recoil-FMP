using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectMenu : MonoBehaviour
{
    private LevelLockManager levelLockManager;

    [SerializeField] Button level1Button;
    [SerializeField] Button level2Button;

    private void Awake()
    {
        DialogueManager.hasSpoken = false;
        DialogueManager.itEnded = false;
    }

    private void OnEnable()
    {
        levelLockManager = FindAnyObjectByType<LevelLockManager>();
    }

    private void Update()
    {
        if (levelLockManager.level1Unlocked)
        {
            level1Button.interactable = true;
        }
        else
        {
            level1Button.interactable = false;
        }

        if (levelLockManager.level2Unlocked)
        {
            level2Button.interactable = true;
        }
        else
        {
            level2Button.interactable = false;
        }
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    // No longer used
    public void TutorialwoWalls()
    {
        SceneManager.LoadScene("TutorialLevel Without Walls");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
}
