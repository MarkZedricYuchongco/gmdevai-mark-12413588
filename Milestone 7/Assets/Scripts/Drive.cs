using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Drive : MonoBehaviour
{
    public bool isDead = false;

    public float speed = 10.0F;
    public float rotationSpeed = 100.0F;

    public float shotCooldown = 0.5f;
    float lastShotTime = 0f;

    public float health = 100f;

    public GameObject explosion;
    public GameObject bullet;
    public GameObject turret;

    public TextMeshProUGUI healthDisp;

    private void Start()
    {
        UpdateHealthUI();
    }

    void Update()
    {
        if (isDead) return;

        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);

        if (Input.GetKey(KeyCode.Space) && lastShotTime > shotCooldown)
        {
            Fire();
            lastShotTime = 0f;
        }
        lastShotTime += Time.deltaTime;
    }

    void Fire()
    {
        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 1000);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            Debug.Log("Ow!");
            health -= 10f;
            UpdateHealthUI();
        }

        if (health <= 0)
        {
            health = 0;
            UpdateHealthUI();
            DoDeath();
        }
    }

    void UpdateHealthUI()
    {
        healthDisp.text = "Health: " + health.ToString();
    }

    void DoDeath()
    {
        if (isDead) return;
        isDead = true;

        GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }

        if (healthDisp != null)
        {
            healthDisp.text = "DESTROYED.";
            healthDisp.color = Color.red;
        }

        Destroy(e, 1.5f);
    }
}