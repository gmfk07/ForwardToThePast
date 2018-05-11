using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public float x;
    public float y;
    public bool time;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.transform.position = new Vector3(x, y, 0);
            if (time)
            {
                var stat = collider.gameObject.GetComponent<PlayerStats>().past;
                stat = !stat;
            }
        }
    }
}
