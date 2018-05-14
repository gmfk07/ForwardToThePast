using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour {

    public float speed;
    //private GameObject cam;
    private Vector3 direction;
    public bool vert = true;

    private void Start()
    {
        //cam = GameObject.Find("Main Camera");
        if (vert)
            direction = Vector3.up;
        else
            direction = Vector3.right;
    }

    // Update is called once per frame
    void Update () {
        //if (GetComponent<Renderer>().isVisible)
        Move();
	}

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
            direction = -direction;
    }

    void Move()
    {
        transform.Translate(direction * speed);
    }
}
