using UnityEngine;
using UnityEngine.InputSystem;

public class FPSController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float lookSensitivity = 1;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Camera playerCamera;
    private float rotationX = 0;
    private Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerCamera = GetComponentInChildren<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * lookSensitivity;
        float mouseY = mouseDelta.y * lookSensitivity;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = 0;
        float moveZ = 0;

        var keyboard = Keyboard.current;
        if (keyboard.wKey.isPressed) moveZ = 1;
        if (keyboard.sKey.isPressed) moveZ = -1;
        if (keyboard.aKey.isPressed) moveX = -1;
        if (keyboard.dKey.isPressed) moveX = 1;

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * moveSpeed * Time.deltaTime);

        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
