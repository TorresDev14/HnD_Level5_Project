using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public float mouseSensitivityXVertical = 100f; // the amount of sensitivity for the mouse in Y direction
    public float mouseSensitivityYHorizontal = 100f; // the amount of sensitivity fot the mouse in X direction

    public Transform playerBody; // to attach the character we want to control his vision

    float xRotation = 0f; // control the mouse in vertical direction

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // avoid appearing the cursor while gaming
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityXVertical * Time.deltaTime; // to connect the sensitivity to the camera control on Horizontal direction
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityYHorizontal * Time.deltaTime; // to connect the sensitivity to the camera control on Vertical direction

        xRotation -= mouseY; // mouse direction up and down not Inverted
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // to block the character's vision on 90º

        
        transform.localRotation = Quaternion.Euler(xRotation + RecoilScript.targetRotation.x, RecoilScript.targetRotation.y, RecoilScript.targetRotation.z) ;
        playerBody.Rotate(Vector3.up * mouseX);

        
    }
}
