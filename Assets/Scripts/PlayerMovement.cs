using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;

    private void FixedUpdate()
    {
        Transform t = GetComponent<Transform>();
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        t.position += new Vector3(horInput * speed, vertInput * speed);
    }
}
