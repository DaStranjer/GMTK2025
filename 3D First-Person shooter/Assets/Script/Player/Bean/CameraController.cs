using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float camRotX = 0f;
    public float camRotY = 0f;
    [SerializeField] public UserPreferences userPreferences;
    // Update is called once per frame
    public Transform Player;
    void Update()
    {
        transform.position = Player.position;
        if (camRotX <= 80 && camRotX >= -80)
        {
            camRotX -= Input.GetAxis("Mouse Y") * userPreferences.sensitivity;
        }
        else if (camRotX >= 80)
        {
            camRotX = 80f;
        }
        else if (camRotX <= -80)
        {
            camRotX = -80f;
        }
        camRotY += Input.GetAxis("Mouse X") * userPreferences.sensitivity;
        transform.localEulerAngles = new Vector3(camRotX, camRotY, 0);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
