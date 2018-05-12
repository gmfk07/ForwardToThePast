using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    public int health = 40;
    public int dropMoney = 1;
    public float dropHealthChance = .1f;
    public float coinMaxSpeed = 200f;
    public float coinMinSpeed = 100f;
    public bool hurtOnContact = true;
    public GameObject coinObject;
    public GameObject healthObject;

    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            Drop();
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

    private void Drop()
    {
        for (var i = 0; i <= dropMoney; i++)
        {
            GameObject created;
            if (i == dropMoney)
            {
                var testvar = Random.value;
                if (testvar < dropHealthChance)
                    created = Instantiate(healthObject) as GameObject;
                else
                    continue;
            }
            else
                created = Instantiate(coinObject) as GameObject;

            var angle = Random.Range(0, 360);
            var speed = Random.Range(coinMinSpeed, coinMaxSpeed);
            created.transform.position = transform.position;
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * speed, Mathf.Cos(angle) * speed));
        }
    }
}
