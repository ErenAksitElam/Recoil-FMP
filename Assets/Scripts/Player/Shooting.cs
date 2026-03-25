using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;

public class Shooting : MonoBehaviour
{
    //[SerializeField, DisplayWithoutEdit] private int whatever = 1; As a template for DisplayWithoutEdit in the future

    public PlayerControls controls;
    private InputAction shooting;
    private InputAction restart;
    private InputAction swap;

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
    [SerializeField, DisplayWithoutEdit] private bool isReloading = false;

    [SerializeField, DisplayWithoutEdit] private bool cooldown = false;
    [SerializeField, DisplayWithoutEdit] private bool hasShot = false;

    [SerializeField] private GameObject bullet;
    [SerializeField] float bulletLife = 3f;

    private Rigidbody rb;

    public int recoilForce;

    public int playerHP;

    public int pistolDamage;
    public int shotgunDamage;

    public int damage;

    public bool pistolAcquired = false;
    public bool shotgunAcquired = false;

    public GameObject playerWillBe;

    public float shotCooldown;

    [DisplayWithoutEdit] public bool shootingDisabled = false;

    [SerializeField, DisplayWithoutEdit] private bool hasWeapon;

    private bool hasSwapped;

    private GameObject bulletInst;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        controls = new PlayerControls();

        /*
        // To set the starting ammo count to that of the pistol
        if (currentWeapon == "Pistol")
        {
            //Debug.Log("Pistol");
            originalAmmo = pistolAmmo;
            ammo = originalAmmo;
            //maxDistance = pistolMaxDistance;
        }
        else //Just incase until a second weapon is added
            return;
        */

        if (currentWeapon == "Pistol")
        {
            damage = pistolDamage;
        }
        else if (currentWeapon == "Shotgun")
        {
            damage = shotgunDamage;
        }

        shootingDisabled = false;
    }

    private void OnEnable()
    {
        shooting = controls.Player.Shooting;
        shooting.Enable();

        restart = controls.Player.Restart;
        restart.Enable();

        swap = controls.Player.Swap;
        swap.Enable();
    }
    
    private void OnDisable()
    {
        shooting.Disable();

        restart.Disable();

        swap.Disable();
    }
    private void Update()
    {
        Firing();

        //So that the player has to press the button repeatedly instead of holding it
        if (shooting.WasReleasedThisFrame())
            hasShot = false;
        if (swap.WasReleasedThisFrame())
            hasSwapped = false;

        playerWillBe.transform.position = rb.linearVelocity + gameObject.transform.position;

        if (playerHP <= 0)
            SceneManager.LoadScene("Game Over");

        LayerMask groundLayer = LayerMask.NameToLayer("Ground");
        bool onGround = Physics.Raycast(transform.position, Vector3.down, 1000, groundLayer);

        if (restart.IsPressed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (swap.IsPressed() && shotgunAcquired && !hasSwapped)
        {
            if (currentWeapon == "Pistol")
            {
                Debug.Log("Swapped to Shotgun");
                currentWeapon = "Shotgun";
                damage = shotgunDamage;
                hasSwapped = true;
            }
            else if (currentWeapon == "Shotgun")
            {
                Debug.Log("Swapped to Pistol");
                currentWeapon = "Pistol";
                damage = pistolDamage;
                hasSwapped = true;
            }
        }
    }

    private void Firing()
    {
        if (shooting.IsPressed() && !isReloading && !cooldown && !hasShot && !shootingDisabled)
        {
            cooldown = true;
            hasShot = true;
            //Debug.Log("Shooted");
            //Instantiating the bullet that is fired
            if (currentWeapon == "Pistol")
            {

                bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
                bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);
                bulletInst.transform.rotation = gameObject.transform.rotation;
            }
            else if (currentWeapon == "Shotgun")
            {

                bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
                bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);
                bulletInst.transform.rotation = gameObject.transform.rotation;
            }

            //The recoil force
            rb.AddForce(-gameObject.transform.right * recoilForce);

            StartCoroutine(shootingCooldown());
            ammo -= 1;
            Destroy(bulletInst, bulletLife);
            //Could be added for the shotgun as that would have a low amount of ammo with high knockback and damage
            /*if (ammo <= 0)
                StartCoroutine(ReloadWait());*/
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
        yield return new WaitForSeconds(shotCooldown);
        cooldown = false;
    }

    public void TakeDamage(int enemyDamage)
    {
        playerHP -= enemyDamage;
    }
}
