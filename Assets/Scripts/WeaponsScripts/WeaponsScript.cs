using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;

public class WeaponsScript : WeaponAnimation
{
    [Header ("Weapon")]
    public float damage = 10f; // the amount of damage the bullet will take
    public float range = 100f; // the amount of distance can the bullet hit the enemy or objects
    public float fireRate = 15f; // set up the fire rate of the weapon
    public float nextTimeToFire = 0f;

    public int magSize = 32; // set up the max ammo for the mag, make it public so we can edit as we want
    public int currentAmmo; // the ammo we have at the moment into the mag
    public int pocketAmmo; //pocketAmmoM4; // all amount of bullets for each weapon
    public float reloadTime = 1f; // to reload the weapon
                                  //public bool isReloading = false;

    public ParticleSystem muzzleEffect;

    public GameObject impactEffect;
    public GameObject muzzleLight;
    public GameObject centreSight;

    public Camera fpsCam;

    public bool isOutOfAmmo;
    public bool isClicking = false;

    [Header ("UI")]
    public TextMeshProUGUI bulletsCounter;


    [Header ("Audio")]
    public AudioSource weaponSoundEffect;
    public AudioSource outOfAmmo;
    public AudioSource emptyPocket;


    //public GameObject bullets10, bullets30, bullets50;


    private RecoilScript recoilScriptgun,recoilscriptcam;


    private void Start()
    {
        muzzleLight = GameObject.Find("PointLight");
        muzzleLight.SetActive(false);
        isOutOfAmmo = false;
        currentAmmo = magSize;
        centreSight.SetActive(true);

        recoilScriptgun = GameObject.Find("Main Camera").GetComponent<RecoilScript>();
    }

    private void OnEnable()
    {
        isReloading = false;
    }


    // Update is called once per frame
    void Update()
    {
        string gunName = this.gameObject.name.ToString();
        bulletsCounter.text = gunName.Substring(0, gunName.Length - 4) + ": " + currentAmmo + "/" + magSize + " (" + pocketAmmo.ToString() + ")";

        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            animator1.SetBool("IsAiming", true);

            isRunning = false;
            animator1.SetBool("IsRunning", false);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
            animator1.SetBool("IsAiming", false);
        }

        if (Input.GetButtonDown("Run"))
        {
            isRunning = !isRunning;
            animator1.SetBool("IsRunning", true);

            isAiming = false;
            animator1.SetBool("IsAiming", false);
        }
        else if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
            animator1.SetBool("IsRunning", false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isSlow = !isSlow;
            animator1.SetBool("IsSlow", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isSlow = false;
            animator1.SetBool("IsSlow", false);
        }

        if (Input.GetButtonDown("Watching"))
        {
            isWatching = !isWatching;
            animator1.SetBool("IsWatching", true);
        }
        else if (Input.GetButtonUp("Watching"))
        {
            isWatching = false;
            animator1.SetBool("IsWatching", false);
        }

        if (Input.GetButtonDown("Reload") && currentAmmo <= (magSize - 1) && pocketAmmo > 0)
        {
            isAiming = false;
            animator1.SetBool("IsAiming", false);

            isReloading = !isReloading;
            animator1.SetBool("IsReloading", true);
            pocketAmmo = pocketAmmo - (magSize - currentAmmo);
            currentAmmo = currentAmmo + (magSize - currentAmmo);
            reloading.Play();

            centreSight.SetActive(false);

            if (pocketAmmo < 1)
            {
                currentAmmo += pocketAmmo;
                pocketAmmo = 0;
            }
        }
        else if (Input.GetButtonUp("Reload"))
        {
            animator1.SetBool("IsReloading", false);
            isReloading = false;
            centreSight.SetActive(true);
        }

        if (currentAmmo == 0)
        {
            isOutOfAmmo = true;
            if (Input.GetButtonDown("Fire1"))
            {
                isClicking = true;
                outOfAmmo.Play();
            }
            return;
        }
        else
            isOutOfAmmo = false;

        if (Input.GetButtonDown("TiltLeft"))
        {
            isTiltLeft = !isTiltLeft;
            animator1.SetBool("IsTiltLeft", true);
        }
        else if (Input.GetButtonUp("TiltLeft"))
        {
            isTiltLeft = false;
            animator1.SetBool("IsTiltLeft", false);
        }

        if (Input.GetButtonDown("TiltRight"))
        {
            isTiltRight = !isTiltRight;
            animator1.SetBool("IsTiltRight", true);
        }
        else if (Input.GetButtonUp("TiltRight"))
        {
            isTiltRight = false;
            animator1.SetBool("IsTiltRight", false);
        }


        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !isReloading) //  to make the the weapon fire while pressing, need to remove the "Down" word from "GetButtonDown" in IF statment
        {
            weaponSoundEffect.Play();
            muzzleEffect.Play();
            muzzleLight.SetActive(true);
            nextTimeToFire = Time.time + 1f / fireRate; // allow to keep fire while pressing the Fire1 button
            Shoot();
        }
        else
            muzzleLight.SetActive(false);
    }


    IEnumerator ReloadAmmo()
    {
        isReloading = false;
        yield return new WaitForSeconds(reloadTime);
    }

    public void Shoot()
    {
        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) // to hit the objects
        {
            DamageToEnemy target = hit.transform.GetComponent<DamageToEnemy>(); // call the script where is the enemy's HP allocated to get the damage works
            if (target != null)
            {
                target.takeDamage(damage); // to take damage to the enemy
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // to make the impact effect of the bullet in the objects or enemies
        }
        
        recoilScriptgun.RecoilFire();
    }

}