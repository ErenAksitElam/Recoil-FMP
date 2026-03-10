using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Shooting : MonoBehaviour
{
    //[SerializeField, DisplayWithoutEdit] private int whatever = 1; As a template for DisplayWithoutEdit in the future

    public PlayerControls controls;
    private InputAction shooting;

    [SerializeField, DisplayWithoutEdit] private int ammo;
    public int pistolAmmo;
    public int shotgunAmmo;

    private int originalAmmo;

    /*
    [SerializeField, DisplayWithoutEdit] private int maxDistance;
    public int pistolMaxDistance;
    public int shotgunMaxDistance;*/

    public string currentWeapon;

    public float bulletSpeed = 90f;
    private bool isReloading = false;
    
    private bool cooldown = false;

    [SerializeField] private GameObject bullet;
    [SerializeField] float bulletLife = 3f;

    private Rigidbody rb;

    public int recoilForce;

    private void Start()
    {
        if (currentWeapon == "Pistol")
        {
            Debug.Log("Pistol");
            originalAmmo = pistolAmmo;
            ammo = originalAmmo;
            //maxDistance = pistolMaxDistance;
        }
        else
            return;

        rb = GetComponent<Rigidbody>();
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
        Debug.Log(isReloading);
    }

    private void Firing()
    {
        if (shooting.IsPressed() && !isReloading && !cooldown)
        {
            cooldown = true;
            Debug.Log("Shooted");
            GameObject bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
            bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);

            rb.AddForce(-gameObject.transform.right * recoilForce);

            StartCoroutine(shootingCooldown());
            ammo -= 1;
            Destroy(bulletInst, bulletLife);
            if (ammo <= 0)
                StartCoroutine(ReloadWait());
        }
    }

    IEnumerator ReloadWait()
    {
        isReloading = true;
        yield return new WaitForSeconds(2.5f);
        ammo = originalAmmo;
        isReloading = false;
    }

    IEnumerator shootingCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }
}
