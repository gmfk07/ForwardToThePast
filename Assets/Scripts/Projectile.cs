using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float timer = 1.0f;
    private GameObject player;

    private void Start()
    {
        player = PlayerManager.Player;
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Wall")
        {
            if (collider.gameObject.tag == "Player")
                player.GetComponent<PlayerStats>().HurtPlayer();
            Destroy(gameObject);
        }
    }
}
