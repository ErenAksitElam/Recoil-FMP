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

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        controls = new PlayerControls();

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

        if (currentWeapon == "Pistol")
        {
            damage = pistolDamage;
        }
        else //Just incase until a second weapon is added
            return;
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

        //So that the player has to press the button repeatedly instead of holding it
        if (shooting.WasReleasedThisFrame())
            hasShot = false;

        playerWillBe.transform.position = rb.linearVelocity + gameObject.transform.position;

        if (playerHP <= 0)
            SceneManager.LoadScene("Game Over");
    }

    private void Firing()
    {
        if (shooting.IsPressed() && !isReloading && !cooldown && !hasShot)
        {
            cooldown = true;
            hasShot = true;
            //Debug.Log("Shooted");
            //Instantiating the bullet that is fired
            GameObject bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
            bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);

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
