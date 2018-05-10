using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    public int health = 40;

    public void TakeDamage()
    {
        Debug.Log("Enemy Lost Health. HP: " + health);
        //TODO: Death when enemy reaches 0 health
        health--;
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            PlayerStats playerstats = collider.gameObject.GetComponent<PlayerStats>();
            playerstats.HurtPlayer();
        }
    }
}
