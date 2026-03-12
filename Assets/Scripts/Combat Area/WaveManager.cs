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
    private Animator exitGateAnimatorAnimator;

    public GameObject combatArea;

    public bool playerEntered;
    public bool playerAllowedToLeave;

    public bool closeGate;

    private void Update()
    {
        //entranceGateAnimator = entranceGate.GetComponent<Animator>;
    }
}
