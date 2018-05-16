using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : NPC {
    public float x;
    public float y;
    public bool time;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    public override void Talk()
    {
        player.transform.position = new Vector3(x, y, 0);
        if (time)
        {
           player.GetComponent<PlayerStats>().past = !player.GetComponent<PlayerStats>().past;
        }
    }
}
