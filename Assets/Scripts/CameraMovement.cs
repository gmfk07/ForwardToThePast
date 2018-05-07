using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform player;
    public float width;
    public float height;
	
	// Update is called once per frame
	void Update () {
        var playerPos = player.transform.position;
        var camPos = gameObject.transform.position;
        if (playerPos.x > camPos.x + width/2)
            MoveCam(width, 0);
        if (playerPos.x < camPos.x - width/2)
            MoveCam(-width, 0);
        if (playerPos.y > camPos.y + height / 2)
            MoveCam(0, height);
        if (playerPos.y < camPos.y - height / 2)
            MoveCam(0, -height);
	}

    void MoveCam(float xmove, float ymove) {
        var camPos = transform;
        camPos.Translate(Vector3.right * xmove);
        camPos.Translate(Vector3.up * ymove);
    }
}
