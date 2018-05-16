using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour {
    public float projectileTimer = 1.5f;
    private int stage = 1;
    public int maxHealth;
    private int health;
    public float stage1ShootTime = 1.0f;
    public float stage1ReloadTime = 0.2f;
    public float stage1Angle = Mathf.PI / 12;
    public float stage2ReloadTime = 0.15f;
    public float stage2Angle = Mathf.PI / 12;
    public float stage3Angle = Mathf.PI / 10;
    public float projectileSpeed = 0.1f;
    public int stage1MagCount = 3;
    public GameObject projectileObject;
    public GameObject artifactObject;
    GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        maxHealth = GetComponent<EnemyBasic>().health;
        StartCoroutine(Stage1());
	}
	
	// Update is called once per frame
	void Update () {
        health = GetComponent<EnemyBasic>().health;
		switch (stage)
        {
            case 1:
                if (health <= maxHealth / 3 * 2)
                {
                    StopAllCoroutines();
                    StartCoroutine(Stage2());
                    stage = 2;
                }
                break;
            case 2:
                if (health <= maxHealth / 3)
                {
                    stage = 3;
                    StopAllCoroutines();
                    StartCoroutine(Stage3());
                }
                break;
        }
	}

    public void PlayerDied()
    {
        health = maxHealth;
        GetComponent<EnemyBasic>().health = health;
        stage = 1;
        StopAllCoroutines();
        StartCoroutine(Stage1());
    }

    IEnumerator Stage1()
    {
        while (true)
        {
            yield return new WaitForSeconds(stage1ShootTime - stage1ReloadTime);
            for (var i = 0; i < stage1MagCount; i++)
            {
                yield return new WaitForSeconds(stage1ReloadTime);
                for (var j = -1; j < 2; j++)
                {
                    var created = Instantiate(projectileObject);
                    created.transform.position = transform.position;
                    var dir = player.transform.position - transform.position;
                    float angle = Vector3.SignedAngle(dir, Vector3.up, Vector3.forward) * Mathf.Deg2Rad;
                    angle += j * stage1Angle;
                    created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * projectileSpeed, Mathf.Cos(angle) * projectileSpeed));
                }
            }
        }
    }

    IEnumerator Stage2()
    {
        var angle = 0f;
        while (true)
        {
            var created = Instantiate(projectileObject);
            created.transform.position = transform.position;
            created.GetComponent<Projectile>().timer = projectileTimer;
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * projectileSpeed, Mathf.Cos(angle) * projectileSpeed));
            angle += stage2Angle;
            yield return new WaitForSeconds(stage2ReloadTime);
        }
    }

    IEnumerator Stage3()
    {
        var angle1 = 0f;
        var angle2 = Mathf.PI;
        while (true)
        {
            var created = Instantiate(projectileObject);
            created.transform.position = transform.position;
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle1) * projectileSpeed, Mathf.Cos(angle1) * projectileSpeed));
            created.GetComponent<Projectile>().timer = projectileTimer;
            created = Instantiate(projectileObject);
            created.transform.position = transform.position;
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle2) * projectileSpeed, Mathf.Cos(angle2) * projectileSpeed));
            created.GetComponent<Projectile>().timer = projectileTimer;
            angle1 += stage2Angle;
            angle2 -= stage3Angle;
            yield return new WaitForSeconds(stage2ReloadTime);
        }
    }

    private void OnDestroy()
    {
        var created = Instantiate(artifactObject);
        created.transform.position = transform.position;
    }
}