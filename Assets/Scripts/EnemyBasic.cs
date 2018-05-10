using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    public int health;
    private PlayerStats playerstats;

    public void Damage()
    {
        health--;
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            playerstats = collider.gameObject.GetComponent<PlayerStats>();
            if (!playerstats.invincible)
            {
                playerstats.health--;
                playerstats.invincible = true;
                playerstats.StartTimer();
            }
        }
    }
}
