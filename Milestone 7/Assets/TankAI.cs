using UnityEngine;

public class TankAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;

    public GameObject explosion;
    public GameObject bullet;
    public GameObject turret;

    public float health = 100f;

    public GameObject GetPlayer()
    {
        return player;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("distance", Vector3.Distance(player.transform.position, this.transform.position));
        anim.SetFloat("health", health);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("bullet"))
        {
            Debug.Log("Ow!");
            health -= 10f;
        }
    }

    void Fire()
    {
        if (player.GetComponent<Drive>().isDead) return;

        GameObject b = Instantiate(bullet, turret.transform.position, turret.transform.rotation);
        b.GetComponent<Rigidbody>().AddForce(turret.transform.forward * 500);
    }

    public void StopFiring()
    {
        CancelInvoke("Fire");
    }

    public void StartFiring()
    {
        InvokeRepeating("Fire", 0.5f, 0.5f);
    }

    public void DoDeath()
    {
        GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
        Destroy(e, 1.5f);

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.enabled = false;
        }

        Destroy(this.gameObject);
    }
}
