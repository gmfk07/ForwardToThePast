using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int money = 0;
    public int health = 0;
    public Artifact artifact;

    private void Start()
    {
        artifact = GetComponent<Artifact>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (artifact != null)
            {
                artifact.Activate();
            }
            collider.gameObject.GetComponent<PlayerStats>().money += money;
            collider.gameObject.GetComponent<PlayerStats>().IncreaseHealth(health);
            Destroy(gameObject);
        }
    }
}
