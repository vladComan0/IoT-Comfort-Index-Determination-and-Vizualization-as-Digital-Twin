using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour
{
    public float mouseSensitivityVar = 5f;
    public Transform playerBody;
    public Canvas myCanvas;
    private float mouseSensitivity;

    float xRotation = 0f;

    void Start()
    {
    }

    void Update()
    {
        if (myCanvas.enabled)
            mouseSensitivity = 0f;
        else
            mouseSensitivity = mouseSensitivityVar;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity; //* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;//* Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        playerBody.Rotate(Vector3.up * mouseX);
    }
}
