using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading;

public class Shooting : MonoBehaviour
{
    //[SerializeField, DisplayWithoutEdit] private int whatever = 1; As a template for DisplayWithoutEdit in the future

    public PlayerControls controls;
    public InputAction shooting;
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
    [SerializeField] private GameObject shotgunBullet;
    [SerializeField] float bulletLife = 3f;

    private Rigidbody rb;

    private int recoilForce;

    public int playerHP;

    public int pistolDamage;
    public int shotgunDamage;

    public int damage;

    public bool pistolAcquired = false;
    public bool shotgunAcquired = false;

    public GameObject playerWillBe;

    private float shotCooldown;
    public float pistolCooldown;
    public int pistolRecoilForce;

    public float shotgunReloadTime;
    public float shotgunCooldown;
    public int shotgunRecoilForce;

    [DisplayWithoutEdit] public bool shootingDisabled = false;

    [SerializeField, DisplayWithoutEdit] private bool hasWeapon;

    private bool hasSwapped;

    private GameObject bulletInst;

    public GameObject HP1;
    public GameObject HP2;
    public GameObject HP3;

    public int bulletAmount = 10;
    public float startAngle = 90f; // Starting Angle
    public float endAngle = 270f; // Ending Angle

    public GameObject deathScreen;

    public Image fill;
    [SerializeField, DisplayWithoutEdit] private float primaryIndicatorTime;
    public GameObject primaryIndicator;

    public GameObject slidingGraphic;
    public GameObject standingGraphic;

    public GameObject nextWaveText;

    public RankingMenu rankingMenu;

    [SerializeField, DisplayWithoutEdit] private bool firstShot;

    //Player sprite parts
    /*
    public GameObject head;
    public GameObject body;

    private Quaternion lookRotation;
    public float turnSpeed;*/

    private void Start()
    {
        //rb = GetComponent<Rigidbody>();
        rb = GetComponentInParent<Rigidbody>();
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
            shotCooldown = pistolCooldown;
            recoilForce = pistolRecoilForce;
        }
        else if (currentWeapon == "Shotgun")
        {
            damage = shotgunDamage;
            shotCooldown = shotgunCooldown;
            recoilForce = shotgunRecoilForce;
            primaryIndicatorTime = shotgunReloadTime;
        }

        primaryIndicatorTime = shotgunReloadTime;

        shootingDisabled = false;

        if (shotgunAcquired)
        {
            primaryIndicator.SetActive(true);
        }

        firstShot = true;
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
        {
            HP2.SetActive(false);
            //SceneManager.LoadScene("Game Over");

            primaryIndicator.SetActive(false);
            nextWaveText.SetActive(false);
            deathScreen.SetActive(true);
        }   

        LayerMask groundLayer = LayerMask.NameToLayer("Ground");
        bool onGround = Physics.Raycast(transform.position, Vector3.down, 1000, groundLayer);

        if (restart.IsPressed())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (swap.IsPressed() && shotgunAcquired)
        {
            /*
            if (currentWeapon == "Pistol")
            {
                Debug.Log("Swapped to Shotgun");
                currentWeapon = "Shotgun";
                damage = shotgunDamage;
                shotCooldown = shotgunCooldown;
                recoilForce = shotgunRecoilForce;
                //primaryIndicatorTime = shotgunReloadTime;
                hasSwapped = true;
            }
            else if (currentWeapon == "Shotgun")
            {
                Debug.Log("Swapped to Pistol");
                currentWeapon = "Pistol";
                damage = pistolDamage;
                shotCooldown = pistolCooldown;
                recoilForce = pistolRecoilForce;
                hasSwapped = true;
            }
            */
            currentWeapon = "Shotgun";
            damage = shotgunDamage;
            shotCooldown = shotgunCooldown;
            recoilForce = shotgunRecoilForce;
            ShotgunFiring();
        }

        if (isReloading)
        {
            primaryIndicatorTime += Time.deltaTime;
            fill.fillAmount = primaryIndicatorTime / shotgunReloadTime;
        }
        else
        {
            primaryIndicatorTime = shotgunReloadTime;
            fill.fillAmount = primaryIndicatorTime;
        }
        /*
        lookRotation = Quaternion.LookRotation((playerWillBe.transform.position - transform.parent.position).normalized);
        lookRotation *= Quaternion.Euler(0, 90, 0);


        head.transform.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * turnSpeed);
        body.transform.rotation = Quaternion.Slerp(transform.parent.rotation, lookRotation, Time.deltaTime * turnSpeed);*/
    }

    private void Firing()
    {
        if (shooting.IsPressed() && !hasShot && !shootingDisabled)
        {
            currentWeapon = "Pistol";
            damage = pistolDamage;
            shotCooldown = pistolCooldown;
            recoilForce = pistolRecoilForce;

            if (firstShot)
            {
                firstShot = false;
                rankingMenu.timerActive = true;
            }
            standingGraphic.SetActive(false);
            slidingGraphic.SetActive(true);
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

                //The recoil force
                rb.AddForce(-gameObject.transform.right * recoilForce);
            }
            else if (currentWeapon == "Shotgun" && !isReloading)
            {
                /*
                bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
                bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);
                bulletInst.transform.rotation = gameObject.transform.rotation;
                */
                primaryIndicatorTime = 0;
                fill.fillAmount = primaryIndicatorTime;

                float angleStep = (endAngle - startAngle) / bulletAmount;
                float angle = startAngle;
                for (int i = 0; i < bulletAmount; i++)
                {
                    
                    //float bulDirY = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad);
                    float bulDirY = (gameObject.transform.eulerAngles.y + angle);
                    //Debug.Log(bulDirY);

                    bulletInst = Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, bulDirY, transform.eulerAngles.z));

                    bulletInst.transform.position = transform.position;

                    Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();

                    Quaternion quatAngles = Quaternion.Inverse(Quaternion.Euler(new Vector3(transform.eulerAngles.x, -bulDirY + 180, transform.eulerAngles.z)));
                    bulletInst.transform.rotation = quatAngles;
                    //bulletInst.transform.eulerAngles = new Vector3(transform.eulerAngles.x, bulDirY, transform.eulerAngles.z);

                    bulletInstRB.AddForce(bulletInst.transform.right * bulletSpeed);
                    angle += angleStep;
                }

                //The recoil force
                rb.AddForce(-gameObject.transform.right * recoilForce);
            }

            ammo -= 1;
            Destroy(bulletInst, bulletLife);
            //Could be added for the shotgun as that would have a low amount of ammo with high knockback and damage
            if (ammo <= 0 && currentWeapon == "Shotgun")
                StartCoroutine(ReloadWait());
            else
                StartCoroutine(shootingCooldown());
        }
    }

    private void ShotgunFiring()
    {
        if (!hasShot && !shootingDisabled)
        {
            if (firstShot)
            {
                firstShot = false;
                rankingMenu.timerActive = true;
            }
            standingGraphic.SetActive(false);
            slidingGraphic.SetActive(true);
            cooldown = true;
            hasShot = true;
            //Debug.Log("Shooted");
            //Instantiating the bullet that is fired
            if (!isReloading)
            {
                /*
                bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
                Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
                bulletInstRB.AddForce(gameObject.transform.right * bulletSpeed);
                bulletInst.transform.rotation = gameObject.transform.rotation;
                */
                primaryIndicatorTime = 0;
                fill.fillAmount = primaryIndicatorTime;

                float angleStep = (endAngle - startAngle) / bulletAmount;
                float angle = startAngle;
                for (int i = 0; i < bulletAmount; i++)
                {

                    //float bulDirY = transform.position.y + Mathf.Sin(angle * Mathf.Deg2Rad);
                    float bulDirY = (gameObject.transform.eulerAngles.y + angle);
                    //Debug.Log(bulDirY);

                    bulletInst = Instantiate(bullet, transform.position, Quaternion.Euler(transform.eulerAngles.x, bulDirY, transform.eulerAngles.z));

                    bulletInst.transform.position = transform.position;

                    Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();

                    Quaternion quatAngles = Quaternion.Inverse(Quaternion.Euler(new Vector3(transform.eulerAngles.x, -bulDirY + 180, transform.eulerAngles.z)));
                    bulletInst.transform.rotation = quatAngles;
                    //bulletInst.transform.eulerAngles = new Vector3(transform.eulerAngles.x, bulDirY, transform.eulerAngles.z);

                    bulletInstRB.AddForce(bulletInst.transform.right * bulletSpeed);
                    angle += angleStep;
                }

                //The recoil force
                rb.AddForce(-gameObject.transform.right * recoilForce);
            }

            ammo -= 1;
            Destroy(bulletInst, bulletLife);
            //Could be added for the shotgun as that would have a low amount of ammo with high knockback and damage
            if (ammo <= 0 && currentWeapon == "Shotgun")
                StartCoroutine(ReloadWait());
            else
                StartCoroutine(shootingCooldown());
        }
    }

    IEnumerator ReloadWait()
    {
        isReloading = true;
        yield return new WaitForSeconds(shotgunReloadTime);
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
        if (playerHP == 2)
        {
            HP3.SetActive(false);
        }
        if (playerHP == 1)
        {
            HP2.SetActive(false);
        }
    }
}
