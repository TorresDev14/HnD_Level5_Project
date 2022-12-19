using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
   
public class PickBullets : WeaponAnimation
{
    public Animator animator2;

    public AudioSource openBox;
    private void Start()
    {

    }

    public void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetButtonDown("Interact"))
        {
            isOpening = true;
            animator2.SetBool("IsOpening", true);
            openBox.Play();
        }
        else if (isOpening == true)
        {
            openBox.Stop();
        }

    }

}
