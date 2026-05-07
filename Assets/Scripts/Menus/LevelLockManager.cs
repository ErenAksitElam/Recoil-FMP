using UnityEngine;

public class LevelLockManager : MonoBehaviour
{
    public bool level1Unlocked;
    public bool level2Unlocked;

    private void Update()
    {
        level1Unlocked = (PlayerPrefs.GetInt("UnlockedLevel1") != 0);
        level2Unlocked = (PlayerPrefs.GetInt("UnlockedLevel2") != 0);
    }

    public void UnlockLevel1()
    {
        level1Unlocked = true;
        PlayerPrefs.SetInt("UnlockedLevel1", (level1Unlocked ? 1 : 0));
    }

    public void UnlockLevel2()
    {
        level2Unlocked = true;
        PlayerPrefs.SetInt("UnlockedLevel2", (level2Unlocked ? 1 : 0));
    }
}
