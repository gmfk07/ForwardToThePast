using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {

    public int health = 40;
    public int dropMoney = 1;
    public float dropMoneyChance = 1f;
    public float dropHealthChance = .1f;
    public float coinMaxSpeed = 200f;
    public float coinMinSpeed = 100f;
    public bool hurtOnContact = true;
    public GameObject coinObject;
    public GameObject healthObject;
    public float knockbackForce = 350f;
    public delegate void OnDeath();
    public event OnDeath onDeath;
    Rigidbody2D enemyRigidBody;

    private void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        health--;
        if (health == 0)
        {
            if (onDeath != null) onDeath(); //special events after death
            Drop();
            Destroy(gameObject);
        }
    }

    public void KnockbackEnemy(Vector3 source, float customForce = -1) {
        if (enemyRigidBody == null) return;
        customForce = (customForce == -1 ? knockbackForce : customForce);
        Vector3 dir = (transform.position - source).normalized;
        enemyRigidBody.AddForce(dir * customForce, ForceMode2D.Impulse);
        Debug.Log("Enemy knockbacked");
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" && hurtOnContact)
        {
            PlayerStats playerstats = collider.gameObject.GetComponent<PlayerStats>();
            playerstats.HurtPlayer();
            playerstats.KnockbackPlayer(transform.position);
        }
    }

    private void Drop()
    {
        for (var i = 0; i <= dropMoney; i++)
        {
            GameObject created;
            var testMoneyVar = Random.value;
            if (i == dropMoney)
            {
                var testHealthVar = Random.value;
                if (testHealthVar <= dropHealthChance)
                {
                    created = Instantiate(healthObject) as GameObject;
                    created.transform.position = transform.position;
                }
                else
                    continue;
            }
            else if (testMoneyVar <= dropMoneyChance)
            {
                created = Instantiate(coinObject) as GameObject;

                var angle = Random.Range(0, 360);
                var speed = Random.Range(coinMinSpeed, coinMaxSpeed);
                created.transform.position = transform.position;
                created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * speed, Mathf.Cos(angle) * speed));
            }
        }
    }
}
