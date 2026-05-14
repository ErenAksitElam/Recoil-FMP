using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
// Repurposed from Year 12 FMP
public class DialogueManager : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogue;
    private int index = 0;

    public float wordSpeed;

    public GameObject pressClickToContinue;

    public string finalLine;

    public AudioSource[] voiceline;

    [SerializeField, DisplayWithoutEdit] private static int armyGuy = 0;

    public Shooting shooting;
    public RankingMenu rankingMenu;

    public GameObject healthBar;
    public GameObject timer;

    public static bool hasSpoken;
    public static bool itEnded;

    void Start()
    {
        dialogueText.text = "";

        healthBar.SetActive(false);
        timer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (armyGuy == 0 && !hasSpoken)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                shooting.shootingDisabled = true;
                dialoguePanel.SetActive(true);
                pressClickToContinue.SetActive(true);
                armyGuy = 1;
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                armyGuy = 1;
                NextLine();
            }
        }
        if (armyGuy == 1 && UnityEngine.Input.GetMouseButtonDown(0) && !hasSpoken)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                shooting.shootingDisabled = true;
                dialoguePanel.SetActive(true);
                pressClickToContinue.SetActive(true);
                armyGuy = 1;
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                armyGuy = 1;
                NextLine();
            }
        }

        if (UnityEngine.Input.GetMouseButtonDown(0) && !hasSpoken)
        {
            if (!dialoguePanel.activeInHierarchy)
            {
                shooting.shootingDisabled = true;
                dialoguePanel.SetActive(true);
                pressClickToContinue.SetActive(true);
                StartCoroutine(Typing());
            }
            else if (dialogueText.text == dialogue[index])
            {
                NextLine();
            }
        }

        if (itEnded)
        {
            shooting.shootingDisabled = false;
            healthBar.SetActive(true);
            timer.SetActive(true);
            hasSpoken = true;
            pressClickToContinue.SetActive(false);
            dialoguePanel.SetActive(false);
        }
    }

    public void RemoveText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        pressClickToContinue.SetActive(false);
    }

    IEnumerator Typing()
    {
        if (index >= 0 && index < voiceline.Length && voiceline[index] != null)
        {
            voiceline[index].Play();
        }
        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(Typing());

            if (dialogue[index] == finalLine)
            {
                dialoguePanel.SetActive(false);
                healthBar.SetActive(true);
                timer.SetActive(true);
                hasSpoken = true;
                pressClickToContinue.SetActive(false);

                itEnded = true;
            }

        }
        else
        {
            armyGuy = 2;
            RemoveText();
        }
    }
}
