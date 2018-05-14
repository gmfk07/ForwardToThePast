using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float timer = 1.0f;

    private void Start()
    {
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }

    void OnCollisionStay2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
        }
    }
}
