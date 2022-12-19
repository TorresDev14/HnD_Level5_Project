using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity;

public class SFX : MonoBehaviour
{
    public AudioSource pickWeapon;
    public AudioSource aK47Sound;
    public AudioSource m4A1Sound;

    public GameObject weaponAK;
    public GameObject weaponM4A1;

    public bool aKEquipped = false;
    public bool m4A1Equipped = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponAK.SetActive(false);
        weaponM4A1.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        FireSystem();
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag.Equals("AK47Floor"))
        {
            aKEquipped = true;
            m4A1Equipped = false;
            weaponAK.SetActive(true);
            pickWeapon.Play();
        }

        if (collision.gameObject.tag.Equals("M4A1Floor"))
        {
            m4A1Equipped = true;
            aKEquipped = false;
            weaponM4A1.SetActive(true);
            pickWeapon.Play();
        }
    }

    public void FireSystem()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (weaponAK.gameObject.Equals(true))
            {
                aK47Sound.Play();
            }
            else if (weaponM4A1.gameObject.Equals(true))
            {
                m4A1Sound.Play();
            }

        }
    }

}
