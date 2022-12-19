using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage; // the amount of damage the bullet will take
    public float range;  // the amount of distance can the bullet hit the enemy or objects
    public float fireRate;
    

    public AudioSource GunSoundEffect;

    public Camera fpsCam;
    //public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    protected float nextTimeTiFire;


    private void Start()
    {
        
    }


    // Update is called once per frame
    protected void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeTiFire)
        {
            nextTimeTiFire = Time.time + 1f / fireRate; // allow to keep fire while pressing the Fire1 button and for that also need to remove the "Down" word from "GetButtonDown" in if statment
            Shoot();
            GunSoundEffect.Play();

        }
    }

     protected void Shoot()
    {
       // muzzleFlash.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) // to hit the objects
        {
            DamageToEnemy target = hit.transform.GetComponent<DamageToEnemy>(); // call the script where is the life of the enemy to get the damage works
            if (target != null)
            {
                target.takeDamage(damage); // to make damage to the enemy

                Debug.Log(hit.transform.name);
            }

            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal)); // to make the impact effect of the bullet in the objects or enemies
        }
    }
}
