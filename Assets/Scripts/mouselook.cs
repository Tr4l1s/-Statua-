using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouselook : MonoBehaviour
{
    public PlayerController playerScrpit;

    public float sense = 100f;

    public Transform body;

    float xrot = 0f;
    float yRotAccum = 0f;

    public Transform leaner;
    public float zRot;
    bool isRotating;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        #region Camera
        float rotx = Input.GetAxis("Mouse X") * sense * Time.deltaTime;
        float rotY = Input.GetAxis("Mouse Y") * sense * Time.deltaTime;

        xrot -= rotY;
        xrot = Mathf.Clamp(xrot, -90f, 90f);
        yRotAccum += rotx;

        Quaternion xQuat = Quaternion.Euler(xrot, 0f, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, xQuat, Time.deltaTime * 10f);

        Quaternion bodyTarget = Quaternion.Euler(0f, yRotAccum, 0f);
        body.rotation = Quaternion.Lerp(body.rotation, bodyTarget, Time.deltaTime * 10f);
        #endregion

        #region Camera Leaner
        if (Input.GetKey(KeyCode.I))
        {
            zRot = Mathf.Lerp(zRot, -20.0f, 5f * Time.deltaTime);
            isRotating = true;
            playerScrpit.canMove = false;
        }

        if (Input.GetKey(KeyCode.L))
        {
            zRot = Mathf.Lerp(zRot, 20.0f, 5f * Time.deltaTime);
            isRotating = true;
            playerScrpit.canMove = false;
        }

        if(Input.GetKeyUp(KeyCode.L) || Input.GetKeyUp(KeyCode.E))
        {
            isRotating= false;
            playerScrpit.canMove = true;
        }
        if (!isRotating)
        {
            zRot = Mathf.Lerp(zRot, 0.0f, 5f * Time.deltaTime);
        }
        
        leaner.localRotation = Quaternion.Euler(0f, 0f, zRot);
        #endregion
    }
}
