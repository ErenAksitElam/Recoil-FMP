using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float turn_speed;

    [SerializeField, DisplayWithoutEdit] public bool playerDetected;
    [SerializeField, DisplayWithoutEdit] private bool enemyAggro;

    public float bulletSpeed = 90f;
    [SerializeField, DisplayWithoutEdit] private bool isReloading = false;

    [SerializeField, DisplayWithoutEdit] private bool cooldown = false;

    [SerializeField] private GameObject bullet;
    [SerializeField] float bulletLife = 3f;

    [SerializeField, DisplayWithoutEdit] private int ammo;
    public int originalAmmo;

    [SerializeField] private float shotCooldown = 0.45f;

    private void Awake()
    {
        ammo = originalAmmo;
    }

    private void Update()
    {
        rotateTowards(target.position);

        if (playerDetected)
        {
            enemyAggro = true;
        }
        EnemyShoot();
    }

    protected void rotateTowards(Vector3 to)
    {

        Quaternion _lookRotation = Quaternion.LookRotation((to - transform.position).normalized);

        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * turn_speed);
    }

    void EnemyShoot()
    {
        if (enemyAggro && !isReloading && !cooldown)
        {
            cooldown = true;
            GameObject bulletInst = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody bulletInstRB = bulletInst.GetComponent<Rigidbody>();
            bulletInstRB.AddForce(gameObject.transform.forward * bulletSpeed);

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
        yield return new WaitForSeconds(shotCooldown);
        cooldown = false;
    }
}
