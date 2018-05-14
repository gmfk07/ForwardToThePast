using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterEnemy : MonoBehaviour {
    public float aggroRange = 5f;
    public float deaggroRange = 10f;
    public float speed = 4f;
    private bool chasing = false;

    GameObject player;
    Rigidbody2D rb;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (!chasing && direction.magnitude <= aggroRange)
            chasing = true;
        if (chasing && direction.magnitude > deaggroRange)
            chasing = false;
        if (chasing)
        {
            rb.AddForce(direction.normalized * speed);
        }
    }
}
