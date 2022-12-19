using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingItemsAndWeapons : MonoBehaviour
{
    public bool isAimingWeapon = false;

    [Header ("AK47")]
    public GameObject aK47Hand;
    public bool aK47InHand = false;

    public GameObject aK47Floor;
    public bool aK47InFloor = true;

    public GameObject aK47Ammo;

    [Header ("M4A1")]
    public GameObject m4A1Hand;
    public bool m4A1InHand = false;

    public GameObject m4A1Floor;
    public bool m4A1InFloor = true;
   
    public GameObject m4A1Ammo;

    [Header ("Audio")]
    public AudioSource pickWeapon;
    public AudioSource bulletsCollected;

    /*[Header("Animation")]
    public Animator cameraAnimation;
    public bool isCrouching;*/


    // Start is called before the first frame update
    void Start()
    {
        aK47Floor.SetActive(true);
        aK47Hand.SetActive(false);

        m4A1Floor.SetActive(true);
        m4A1Hand.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Crouch"))
        {
            isCrouching = !isCrouching;
            cameraAnimation.SetBool("IsSlow", true);
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            isCrouching = false;
            cameraAnimation.SetBool("IsSlow", false);
        }*/
    }


    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("AK47Floor"))
        {
            pickWeapon.Play();

            aK47InHand = true;
            aK47Hand.SetActive(true);
            aK47InFloor = false;
            aK47Floor.SetActive(false);

            m4A1Hand.SetActive(false);
            m4A1InHand.Equals(false);
            m4A1Floor.SetActive(true);
            m4A1InFloor.Equals(true);
        }

        if (collision.gameObject.CompareTag("M4A1Floor"))
        {
            pickWeapon.Play();

            m4A1InHand = true;
            m4A1Hand.SetActive(true);
            m4A1InFloor = false;
            m4A1Floor.SetActive(false);

            aK47Hand.SetActive(false);
            aK47InHand.Equals(false);
            aK47Floor.SetActive(true);
            aK47InFloor.Equals(true);
        }

        if (collision.gameObject.tag == "AK47Bullets")
        {
            GameObject.FindGameObjectWithTag("AK47").GetComponent<WeaponsScript>().pocketAmmo += 20;
            bulletsCollected.Play();
            Destroy(aK47Ammo.gameObject);
        }

        if (collision.gameObject.tag == "M4A1Bullets")
        {
            GameObject.FindGameObjectWithTag("M4A1").GetComponent<WeaponsScript>().pocketAmmo += 40;
            bulletsCollected.Play();
            Destroy(m4A1Ammo.gameObject);

            return;
        }

        /*if (collision.gameObject.tag == "M4A1Bullets")
        {
            GameObject.FindGameObjectWithTag("M4A1").GetComponent<WeaponsScript>().pocketAmmoM4 += 20;
            Destroy(m4A1Ammo.gameObject);
        }*/
    }
}
