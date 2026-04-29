using UnityEngine;
using TMPro;
using System;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RankingMenu : MonoBehaviour
{
    public bool playerFinished;
    private bool firstTime = true;

    public bool timerActive;
    private float currentTime;
    public TMP_Text timerText;

    public GameObject rankingMenu;

    public GameObject S;
    public GameObject A;
    public GameObject B;
    public GameObject C;
    public GameObject D;
    public GameObject F;

    public float STime;
    public float ATime;
    public float BTime;
    public float CTime;
    public float DTime;

    public TMP_Text rankingMenuTimer;

    private Shooting player;

    private void Awake()
    {
        player = FindFirstObjectByType<Shooting>();
    }

    private void Start()
    {
        currentTime = 0;

        timerActive = true;
    }

    void Update()
    {
        if (playerFinished && firstTime)
        {
            OpenRankingMenu();
        }

        if (timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //text.text = currentTime.ToString();
        timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

        //Debug.Log(currentTime);

    }

    void OpenRankingMenu()
    {
        //Time.timeScale = 0f;
        player.gameObject.GetComponent<LookTowardsMouse>().enabled = false;
        player.shootingDisabled = true;
        timerActive = false;

        timerText.transform.gameObject.SetActive(false);
        rankingMenu.SetActive(true);

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        rankingMenuTimer.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();

        #region CurrentTime < XTime
        if (currentTime < STime)
        {
            TurnOffAllLetters();
            S.SetActive(true);
        }
        else if (currentTime < ATime)
        {
            TurnOffAllLetters();
            A.SetActive(true);
        }
        else if (currentTime < BTime)
        {
            TurnOffAllLetters();
            B.SetActive(true);
        }
        else if (currentTime < CTime)
        {
            TurnOffAllLetters();
            C.SetActive(true);
        }
        else if (currentTime < DTime)
        {
            TurnOffAllLetters();
            D.SetActive(true);
        }
        else
        {
            TurnOffAllLetters();
            F.SetActive(true);
        }
        #endregion
    }

    public void StartTimer()
    {
        timerActive = true;
    }
    private void StopTimer()
    {
        timerActive = false;
    }

    public void TurnOffAllLetters()
    {
        S.SetActive(false);
        A.SetActive(false);
        B.SetActive(false);
        C.SetActive(false);
        D.SetActive(false);
    }

    public void BackToLevelSelect()
    {
        SceneManager.LoadScene("Level Select Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
