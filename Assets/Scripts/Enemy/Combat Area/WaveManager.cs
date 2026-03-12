using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public GameObject enemy;
    public int numberOfWaves;
    public GameObject[] Wave1Locations;
    public GameObject[] Wave2Locations;
    public GameObject[] Wave3Locations;
    public GameObject[] Wave4Locations;

    public GameObject entranceGate;
    public GameObject exitGate;

    private Animator entranceGateAnimator;
    private Animator exitGateAnimator;

    public GameObject combatArea;

    public bool playerEntered;
    public bool playerAllowedToLeave;

    private void Awake()
    {
        entranceGateAnimator = entranceGate.GetComponent<Animator>();
        exitGateAnimator = exitGate.GetComponent<Animator>();

        entranceGateAnimator.SetBool("CloseGate", false);
        exitGateAnimator.SetBool("CloseGate", true);
    }
    private void Update()
    {
        if (playerEntered && !playerAllowedToLeave)
            entranceGateAnimator.SetBool("CloseGate", true);
        if (playerAllowedToLeave)
        {
            entranceGateAnimator.SetBool("CloseGate", false);
            exitGateAnimator.SetBool("CloseGate", false);
        }
    }
}
