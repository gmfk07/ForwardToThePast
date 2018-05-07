using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyMovement : MonoBehaviour {

    public float speed;
    public GameObject cam;
    private Vector3 direction;

    private void Start()
    {
        direction = Vector3.up;
    }

    // Update is called once per frame
    void Update () {
        if (GetComponent<Renderer>().isVisible)
            Move();
	}

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Wall")
            direction = -direction;
    }

    void Move()
    {
        var camh = cam.GetComponent<CameraMovement>().height;
        var boxh = gameObject.GetComponent<BoxCollider2D>().size.y;
        //var camw = cam.GetComponent<CameraMovement>().width;
        if (transform.position.y + boxh*2 >= cam.transform.position.y + camh / 2 ||
            transform.position.y - boxh*2 <= cam.transform.position.y - camh / 2)
            direction = -direction;
        transform.Translate(direction * speed);
    }
}
