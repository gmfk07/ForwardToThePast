using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour {
    
    public float battleStartRange = 10f;
    public float slashTime;
    public float chaseSpeed = 4f;
    public float runSpeed = 8f;
    public float runTime = 1f;
    public int maxHealth;
    public GameObject slashObject;

    Rigidbody2D rb;
    GameObject player;
    BossState state;

    bool hitPlayer = false;

    // Use this for initialization
    void Start () {
        maxHealth = GetComponent<EnemyBasic>().health;
        rb = GetComponent<Rigidbody2D>();
        player = PlayerManager.instance.Player;
        state = BossState.Idle;
        StartCoroutine(SlashTimer());
	}

    public void SucessfullyHitPlayer() {
        hitPlayer = true;
    }

	// Update is called once per frame
	void Update () {
        Vector3 direction;
        switch (state) {

            case BossState.Idle:
                float dist = (player.transform.position - transform.position).magnitude;
                if (dist < battleStartRange)
                {
                    state = BossState.Chase;
                    StartCoroutine(SlashTimer());
                }
                break;
            case BossState.Chase:
                direction = player.transform.position - transform.position;
                rb.AddForce(direction.normalized * chaseSpeed);
                if (hitPlayer) 
                {
                    StopCoroutine(SlashTimer());
                    StopAllCoroutines();
                    state = BossState.Run;
                    StartCoroutine(RunTimer());
                }
                break;
            case BossState.Run:
                direction = transform.position - player.transform.position;
                rb.AddForce(direction.normalized * runSpeed);
                break;
        }
        hitPlayer = false;
	}

    IEnumerator SlashTimer()
    {
        while (state == BossState.Chase)
        {
            yield return new WaitForSeconds(slashTime);
            if (state != BossState.Chase) yield return null;
            Quaternion angle = Quaternion.Euler(new Vector3(0, 0, 90 + Vector3.SignedAngle(Vector3.up, transform.position - player.transform.position, Vector3.forward)));
            GameObject created = Instantiate(slashObject, transform.position, angle);
            created.transform.parent = gameObject.transform;
        }
    }

    IEnumerator RunTimer()
    {
        yield return new WaitForSeconds(runTime);
        state = BossState.Chase;
        StartCoroutine(SlashTimer());
    }
}

public enum BossState { Idle, Chase, Run }
