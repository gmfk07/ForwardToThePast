using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour {

    private int stage = 1;
    private int maxHealth;
    private int health;
    public float stage1ShootTime = 1.0f;
    public float stage1ReloadTime = 0.2f;
    public int stage1MagCount = 3;
    public GameObject projectileObject;
    private GameObject player;

	// Use this for initialization
	void Start () {
        maxHealth = GetComponent<EnemyBasic>().health;
        health = maxHealth;
        player = PlayerManager.Player;
	}
	
	// Update is called once per frame
	void Update () {
		switch (stage)
        {
            case 1:
                if (health <= maxHealth / 3 * 2)
                    stage = 2;
                break;
            case 2:
                if (health <= maxHealth / 3)
                    stage = 3;
                break;
        }
	}

    IEnumerator Stage1()
    {
        yield return new WaitForSeconds(stage1ShootTime - stage1ReloadTime);
        for (var i = 0; i < stage1MagCount; i++)
        {
            yield return new WaitForSeconds(stage1ReloadTime);
            var created = Instantiate(projectileObject);
            created.transform.position = transform.position;
            Vector3 dir = player.transform.position - transform.position;

        }
    }

}