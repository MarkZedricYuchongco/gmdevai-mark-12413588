using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 10;

    [SerializeField] private float rotSpeed = 50f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        HandlePlayerRotation();
    }

    void HandlePlayerRotation()
    {
        float rotation = Input.GetAxis("Mouse X");
        transform.RotateAround(this.transform.position, Vector3.up, rotation * rotSpeed);
    }
}
