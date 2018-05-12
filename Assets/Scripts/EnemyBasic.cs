using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    public int health = 40;
    public int dropMoney = 1;
    public float coinMaxSpeed = 200f;
    public float coinMinSpeed = 100f;
    public bool hurtOnContact = true;
    public GameObject coinObject;

    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            DropCash();
            Destroy(gameObject);
        }
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && hurtOnContact)
        {
            PlayerStats playerstats = collider.gameObject.GetComponent<PlayerStats>();
            playerstats.HurtPlayer();
        }
    }

    private void DropCash()
    {
        for (var i = 0; i < dropMoney; i++)
        {
            var angle = Random.Range(0, 360);
            var speed = Random.Range(coinMinSpeed, coinMaxSpeed);
            var created = Instantiate(coinObject) as GameObject;
            created.transform.position = transform.position;
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * speed, Mathf.Cos(angle) * speed));
        }
    }
}
