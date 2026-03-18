using UnityEngine;
using TMPro;

public class RankingMenu : MonoBehaviour
{
    public bool playerFinished;
    private bool firstTime = true;

    void Update()
    {
        if (playerFinished && firstTime)
        {
            OpenRankingMenu();
        }

    }

    void OpenRankingMenu()
    {
        Time.timeScale = 0f;
    }

    //https://medium.com/@eveciana21/creating-a-stopwatch-timer-in-unity-f4dff748030d
    //https://github.com/LeiQiaoZhi/Easy-Text-Effects-for-Unity

    //Ramblings of a madman
    //For the dangling I can find when the mouse is in a certain area ie on the ui element and detect it's position compared to the objects position
    //then rotate the object and move it based on time.deltatime and a pivot point so it kinda looks like it's dangling from the ceiling
}
