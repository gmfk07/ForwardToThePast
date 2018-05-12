using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float width;
    public float height;
	
	// Update is called once per frame
	void FixedUpdate () {
        var playerPos = player.transform.position;
        var camPos = gameObject.transform.position;
        camPos = playerPos;
	}

    void MoveCam(float xmove, float ymove) {
        var camPos = transform;
        camPos.Translate(Vector3.right * xmove);
        camPos.Translate(Vector3.up * ymove);
    }
}
