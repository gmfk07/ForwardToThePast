using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour {
    public float shootRange = 6.5f;
    public float runRange = 4f;
    public float stopRange = 6f;
    public float speed = 18f;
    public float shootFreq = 1f;
    public GameObject projectileObject;
    public float projectileSpeed = 4f;
    public float projectileTimer = 2f;
    private bool running = false;
    private GameObject player;
    private bool shooting = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootFreq);
            if (shooting)
            {
                var created = Instantiate(projectileObject);
                created.transform.position = transform.position;
                Vector3 dir = player.transform.position - transform.position;
                created.GetComponent<Rigidbody2D>().AddForce(dir.normalized * projectileSpeed);
                created.GetComponent<Projectile>().timer = projectileTimer;
            }
        }
    }

	void FixedUpdate () {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude <= shootRange && !shooting)
            shooting = true;
        if (direction.magnitude > shootRange && shooting)
            shooting = false;
        if (direction.magnitude <= runRange && !running)
            running = true;
        if (direction.magnitude > runRange && running)
            running = false;
        if (running)
            GetComponent<Rigidbody2D>().AddForce(-direction.normalized * speed);
    }
}