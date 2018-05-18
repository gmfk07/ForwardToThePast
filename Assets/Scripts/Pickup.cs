using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int money = 0;
    public int health = 0;
    public bool maxHealth = false;
    public AudioClip clip;
    public Artifact artifact;

    private void Start()
    {
        artifact = GetComponent<Artifact>();
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            var source = collider.gameObject.GetComponent<AudioSource>();
            source.clip = clip;
            source.Play();
            if (artifact != null)
            {
                artifact.Activate();
            }
            var stats = collider.gameObject.GetComponent<PlayerStats>();
            stats.money += money;
            if (maxHealth)
                stats.maxHealth += health;
            stats.IncreaseHealth(health);
            Destroy(gameObject);
        }
    }
}
