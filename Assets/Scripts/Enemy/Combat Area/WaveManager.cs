using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemy;
    public int numberOfWaves;
    [SerializeField, DisplayWithoutEdit] private int currentWave = 1;

    public GameObject[] wave1Locations;
    public GameObject[] wave2Locations;
    public GameObject[] wave3Locations;
    public GameObject[] wave4Locations;

    private GameObject[] waveLocations;
    private GameObject[] nextWaveLocations;
    private GameObject[] lastWaveLocations;

    public GameObject[] entranceGates;
    public GameObject[] exitGates;

    private Animator entranceGateAnimator;
    private Animator exitGateAnimator;

    public GameObject combatArea;

    public bool playerEntered;
    public bool playerAllowedToLeave;

    [SerializeField, DisplayWithoutEdit] private bool waveEnded = false;

    //[SerializeField, DisplayWithoutEdit] private bool waveCooldown;
    public float waveCooldownTime;

    [DisplayWithoutEdit]  public List<GameObject> instEnemies = new();

    [DisplayWithoutEdit]  public List<GameObject> instIndicators = new();

    public int enemyCount;

    private bool firstStart = true;

    [SerializeField, DisplayWithoutEdit] private int waveLocIndex = -1;

    public GameObject enemyIndicator;


    private void Awake()
    {
        //The entrance gate will be open at the beginning and the exit gate closed.
        foreach (GameObject i in entranceGates)
        {
            entranceGateAnimator = i.GetComponent<Animator>();
            entranceGateAnimator.SetBool("CloseGate", false);
        }
        foreach (GameObject i in exitGates)
        {
            exitGateAnimator = i.GetComponent<Animator>();
            exitGateAnimator.SetBool("CloseGate", true);
        }

        //StartWave();

        currentWave = 1;
}
    private void Update()
    {
        //The entrance gate closes behind the player once they enter the combat area
        if (playerEntered && !playerAllowedToLeave)
        {
            foreach (GameObject i in entranceGates)
            {
                entranceGateAnimator = i.GetComponent<Animator>();
                entranceGateAnimator.SetBool("CloseGate", true);
            }
            foreach (GameObject i in exitGates)
            {
                exitGateAnimator = i.GetComponent<Animator>();
                exitGateAnimator.SetBool("CloseGate", true);
            }

            if (firstStart)
            {
                StartWave();
                firstStart = false;
            }
        }
            
        //Once the player defeats the waves they are able to leave
        if (playerAllowedToLeave)
        {
            foreach (GameObject i in entranceGates)
            {
                entranceGateAnimator = i.GetComponent<Animator>();
                entranceGateAnimator.SetBool("CloseGate", false);
            }
            foreach (GameObject i in exitGates)
            {
                exitGateAnimator = i.GetComponent<Animator>();
                exitGateAnimator.SetBool("CloseGate", false);
            }
        }

        //Spawn a new wave once the old one is finished
        if (waveEnded)
        {
            waveEnded = false;
            currentWave += 1;
            enemyCount = 0;
            if (currentWave > numberOfWaves)
                playerAllowedToLeave = true;
            else
                StartWave();
        }

        if (waveLocations != null)
        {
            if (enemyCount >= waveLocations.Length)
                waveEnded = true;
        }
    }

    void StartWave()
    {
        waveLocIndex = 0;
        //waveCooldown = true;
        waveEnded = false;
        if (currentWave == 1) 
        {
            lastWaveLocations = wave1Locations;
            waveLocations = wave1Locations;
            nextWaveLocations = wave2Locations;
        }
        else if (currentWave == 2) 
        {
            lastWaveLocations = wave1Locations;
            waveLocations = wave2Locations;
            nextWaveLocations = wave3Locations;
        }
        else if (currentWave == 3) 
        {
            lastWaveLocations = wave2Locations;
            waveLocations = wave3Locations;
            nextWaveLocations = wave4Locations;
        }
        else if (currentWave == 4) 
        {
            lastWaveLocations = wave3Locations;
            waveLocations = wave4Locations;
            nextWaveLocations = wave4Locations;
        }
        foreach (GameObject i in waveLocations)
        {
            instEnemies.Add(Instantiate(enemy, i.transform) as GameObject);
        }

        if (currentWave != numberOfWaves)
        {
            //nextWaveLocations[waveLocIndex].GetComponentInChildren<SpriteRenderer>().gameObject.SetActive(true);
            //instIndicators.Add(Instantiate(enemyIndicator, i.transform) as GameObject);
            foreach (GameObject i in nextWaveLocations)
            {
                instIndicators.Add(Instantiate(enemyIndicator, i.transform) as GameObject);
            }
        }

        if (currentWave != 1)
        {
            //lastWaveLocations[waveLocIndex].transform.Find("Next Wave Crosshair").gameObject.SetActive(false);
            foreach (GameObject i in lastWaveLocations)
            {
                waveLocIndex += 1;
                Destroy(instIndicators[waveLocIndex].transform.gameObject);
            }
        }
        else if (currentWave >= numberOfWaves)
        {
            foreach (GameObject i in instIndicators)
            {
                Destroy(i);
            }
        }
    }

    /*
    IEnumerator waveWait()
    {
        waveCooldown = true;
        yield return new WaitForSeconds(waveCooldownTime);
        waveCooldown = false;
    }*/
}
