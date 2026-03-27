using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] public GameObject player;

    private Vector3 CameraOffset;

    [SerializeField] private float rotSpeed = 50f;

    void Start()
    {
        CameraOffset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        MoveCamera();

        HandleCameraRotation();
    }

    void MoveCamera()
    {
        
        transform.position = player.transform.position + CameraOffset;
    }

    void HandleCameraRotation()
    {
        float rotation = Input.GetAxis("Mouse X");
        transform.RotateAround(player.transform.position, Vector3.up, rotation * rotSpeed);
        CameraOffset = transform.position - player.transform.position;
    }
}
