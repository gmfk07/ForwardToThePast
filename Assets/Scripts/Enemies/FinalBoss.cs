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
    public Sprite leftPast;
    public Sprite downPast;
    public Sprite rightPast;
    public Sprite upPast;
    public Sprite leftFuture;
    public Sprite downFuture;
    public Sprite rightFuture;
    public Sprite upFuture;
    public Dialogue deathDialogue;

    SpriteRenderer sr;
    Rigidbody2D rb;
    GameObject player;
    BossState state;
    PlayerStats ps;
    EnemyBasic enemy;

    bool hitPlayer = false;

    // Use this for initialization
    void Start () {
        maxHealth = GetComponent<EnemyBasic>().health;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        enemy = GetComponent<EnemyBasic>();
        player = PlayerManager.instance.Player;
        ps = player.GetComponent<PlayerStats>();
        state = BossState.Idle;
        StartCoroutine(SlashTimer());
        if (ps.past)
            sr.sprite = downPast;
        else
            sr.sprite = downFuture;
        enemy.onDeath += Death;
	}

    public void SucessfullyHitPlayer() {
        hitPlayer = true;
    }

    public void Death() {
        DialogueManager.instance.StartDialogue(deathDialogue);
    }

	// Update is called once per frame
	void FixedUpdate () {
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
        float ang = Vector3.SignedAngle(player.transform.position - transform.position, Vector3.up, Vector3.forward);
        if (!ps.past)
        {
            if (ang < 45 && ang > -45)
                sr.sprite = upPast;
            if (ang >= 45 && ang <= 135)
                sr.sprite = rightPast;
            if (ang > 135 || ang < -135)
                sr.sprite = downPast;
            if (ang > -135 && ang < -45)
                sr.sprite = leftPast;
        } else
        {
            if (ang < 45 && ang > -45)
                sr.sprite = upFuture;
            if (ang >= 45 && ang <= 135)
                sr.sprite = rightFuture;
            if (ang > 135 || ang < -135)
                sr.sprite = downFuture;
            if (ang > -135 && ang < -45)
                sr.sprite = leftFuture;
        }
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
