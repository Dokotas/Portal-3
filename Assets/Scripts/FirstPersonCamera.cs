using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform player;
    float cameraVerticalRotation = 0f;
    
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    
    void Update()
    {
        float inputX = Input.GetAxis("Mouse X")*Settings.MouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y")*Settings.MouseSensitivity;
        
        cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;
        
        player.Rotate(Vector3.up * inputX);
       
    }
}