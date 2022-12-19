using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 3f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        if (isGrounded == true)
        {
            speed = 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (isGrounded == true)
        {
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        OnLandSpeed();
        SlowSpeed();
       

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    public void OnLandSpeed()
    {
        if (isGrounded == true)
        {
            if (Input.GetButtonDown("Run"))
            {
                speed = speed * 2f;
            }
        }
        if (Input.GetButtonUp("Run"))
        {
            speed = 3f;
        }
    }

    public void SlowSpeed()
    {
        if (isGrounded == true)
        {
            if (Input.GetButtonDown("Crouch"))
            {
                speed = speed / 3f;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                speed = 3f;
            }
        }
    }
}
