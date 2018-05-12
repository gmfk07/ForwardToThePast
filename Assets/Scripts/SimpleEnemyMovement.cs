using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour {

    public float speed;
    //private GameObject cam;
    private Vector3 direction;

    private void Start()
    {
        //cam = GameObject.Find("Main Camera");
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update () {
        //if (GetComponent<Renderer>().isVisible)
        Move();
	}

    private void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
            direction = -direction;
    }

    void Move()
    {
        transform.Translate(direction * speed);
    }
}
