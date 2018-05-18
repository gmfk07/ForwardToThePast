using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Only used with FinalBoss
public class EnemySlash : MonoBehaviour {

	// Update is called once per frame
	void Start () {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<PlayerStats>().HurtPlayer();
            gameObject.GetComponentInParent<FinalBoss>().SucessfullyHitPlayer();
        }
    }
}
