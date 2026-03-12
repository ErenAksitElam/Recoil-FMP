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

    public GameObject entranceGate;
    public GameObject exitGate;

    private Animator entranceGateAnimator;
    private Animator exitGateAnimator;

    public GameObject combatArea;

    public bool playerEntered;
    public bool playerAllowedToLeave;

    [SerializeField, DisplayWithoutEdit] private bool waveEnded;

    private void Awake()
    {
        entranceGateAnimator = entranceGate.GetComponent<Animator>();
        exitGateAnimator = exitGate.GetComponent<Animator>();

        //The entrance gate will be open at the beginning and the exit gate closed.
        entranceGateAnimator.SetBool("CloseGate", false);
        exitGateAnimator.SetBool("CloseGate", true);

        StartWave();
    }
    private void Update()
    {
        //The entrance gate closes behind the player once they enter the combat area
        if (playerEntered && !playerAllowedToLeave)
        {
            entranceGateAnimator.SetBool("CloseGate", true);
            exitGateAnimator.SetBool("CloseGate", true);
        }
            
        //Once the player defeats the waves they are able to leave
        if (playerAllowedToLeave)
        {
            entranceGateAnimator.SetBool("CloseGate", false);
            exitGateAnimator.SetBool("CloseGate", false);
        }
            
    }

    void StartWave()
    {
        waveEnded = false;
        if (currentWave == 1) waveLocations = wave1Locations;
        else if (currentWave == 2) waveLocations = wave2Locations;
        else if (currentWave == 3) waveLocations = wave3Locations;
        else if (currentWave == 4) waveLocations = wave4Locations;
        foreach (GameObject i in waveLocations)
        {
            Instantiate(enemy, i.transform);
        }
    }
}
