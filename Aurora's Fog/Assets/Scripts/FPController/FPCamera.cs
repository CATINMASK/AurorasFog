using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Основной скрипт камеры. Отвечает за камеру (голову) персонажа.
public class FPCamera : MonoBehaviour
{
    private FPController fpController;
    [SerializeField] private float mouseSensitivity = 60f;
    [SerializeField] private Transform playerBody;
    //public Transform playerHead;
    //public GameObject headCube;

    [HideInInspector] public bool lockX;
    public float xClampMin = -90f;
    public float xClampMax = 90f;

    private float xAxisClamp;

    private void Awake()
    {
        xAxisClamp = 0.0f;
        if (PlayerPrefs.GetFloat("mouseSensitivity") != 0)
            mouseSensitivity = PlayerPrefs.GetFloat("mouseSensitivity");

        fpController = playerBody.GetComponent<FPController>();
    }

    private void Update()
    {
        if (fpController.canControl)
            CameraRotation();
    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;    // Vertical
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;    // Horizontal

        xAxisClamp += mouseY;

        if (xAxisClamp > xClampMax)
        {
            xAxisClamp = xClampMax;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(360.0f - xClampMax);
        }
        else if (xAxisClamp < xClampMin)
        {
            xAxisClamp = xClampMin;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(-xClampMin);
        }

        transform.Rotate(Vector3.left * mouseY);        // Head rotation
        if (!lockX)
            playerBody.Rotate(Vector3.up * mouseX);     // Body rotation

        //playerHead.rotation = Camera.main.transform.rotation;           // head rotation = camera rotation
        //Camera.main.transform.position = headCube.transform.position;   // camera position = headCube position
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}