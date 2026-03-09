using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooting : MonoBehaviour
{
    //[SerializeField, DisplayWithoutEdit] private int whatever = 1; As a template for DisplayWithoutEdit in the future

    public PlayerControls controls;
    private InputAction shooting;

    [SerializeField, DisplayWithoutEdit] private int ammo;
    [SerializeField, DisplayWithoutEdit] private int maxDistance;
    public int pistolAmmo;
    public int shotgunAmmo;

    public int pistolMaxDistance;
    public int shotgunMaxDistance;

    public string[] weapons;

    private void Start()
    {
        if (weapons.Length == 1)
        {
            ammo = pistolAmmo;
            maxDistance = pistolMaxDistance;
        }
        else
            return;
    }

    private void Awake()
    {
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        shooting = controls.Player.Shooting;
        shooting.Enable();
    }
    
    private void OnDisable()
    {
        shooting.Disable();
    }
    private void Update()
    {
        Firing();
    }

    private void Firing()
    {
        if (shooting.IsPressed())
        {
            Debug.Log("Shooted");

        }
    }
}
