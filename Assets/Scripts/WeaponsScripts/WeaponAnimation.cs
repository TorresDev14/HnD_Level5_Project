using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour
{
    [Header("Animations")]
    public Animator animator1;
    //public Animator animator2;
    public bool isAiming = false;
    public bool isRunning = false;
    public bool isSlow = false;
    public bool isReloading = false;
    public bool isOpening = false;
    public bool isWatching = false;
    public bool onTheGround;

    /*[Header("Camera View Sniper")]
    public GameObject weaponCamera;*/
    //public int ammo;

    [Header("Audio")]
    public AudioSource reloading;

    void Start()
    {

    }

    void Update()
    {
       /* if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            animator.SetBool("IsAiming", true);
        }
        else if (Input.GetButtonUp("Fire2"))
        {
            isAiming = false;
            animator.SetBool("IsAiming", false);
        }

        if (Input.GetButtonDown("Run"))// && onTheGround == true)
        {
            isRunning = !isRunning;
            animator.SetBool("IsRunning", true);
        }

        else if (Input.GetButtonUp("Run"))
        {
            isRunning = false;
            animator.SetBool("IsRunning", false);
        }

        if (Input.GetButtonDown("Crouch"))
        {
            isSlow = !isSlow;
            animator.SetBool("IsSlow", true);
        }

        else if (Input.GetButtonUp("Crouch"))
        {
            isSlow = false;
            animator.SetBool("IsSlow", false);
        }

        if (Input.GetButtonDown("Reload"))
        {
            isReloading = !isReloading;
            animator.SetBool("IsReloading", true);    
            reloading.Play();
        }
        else if (Input.GetButtonUp("Reload"))
        {
            isReloading = false;
            animator.SetBool("IsReloading", false);
        }*/
    }

    /*public void  FinishReload()
    {
        isReloading= false;
        animator.SetBool("IsReloading", isReloading);
    }*/

}
