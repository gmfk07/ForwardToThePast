using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int maxHealth = 3;
    private int health = 3;
    public float height;
    public bool past;
    public bool invincible;
    public int money = 0;
    public float invincibleTimer;
    public float maxInvincibleTimer;
    public float spawnx;
    public float spawny;
    private Renderer playerRenderer;

    private void Start()
    {
        playerRenderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        //Update invincibility
        if (invincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0)
            {
                invincible = false;
            }
        }

        //Die die die
        if (health == 0)
        {
            transform.position = new Vector3(spawnx, spawny, transform.position.z);
            health = maxHealth;
            money = 0;
            var bossList = GameObject.FindGameObjectsWithTag("Boss");
            for (int i = 0; i < bossList.Length; i++)
            {
                if (bossList[i].GetComponent<Golem>() != null)
                {
                    bossList[i].GetComponent<Golem>().PlayerDied();
                }
                else
                    bossList[i].GetComponent<EnemyBasic>().health = bossList[i].GetComponent<FinalBoss>().maxHealth;
            }
        }
    }

    public void HurtPlayer(int damage = 1) {
        if (!invincible)
        {
            health-= 1;
            //TODO: Die when health reaches 0
            invincible = true;
            StartTimer();
        }
    }

    IEnumerator Flash()
    {
        while (invincible)
        {
            playerRenderer.enabled = !playerRenderer.enabled;
            yield return new WaitForSeconds(.05f);
        }
        playerRenderer.enabled = true;
    }

    public void StartTimer()
    {
        invincibleTimer = maxInvincibleTimer;
        StopAllCoroutines();
        playerRenderer.enabled = true;
        StartCoroutine(Flash());
    }

    public void IncreaseHealth(int amt)
    {
        health = Mathf.Min(health + amt, maxHealth);
    }

    private void OnGUI()
    {
        string text = GetTimeString();

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        //Make the HUD
        GUI.BeginGroup(new Rect(0, 0, Screen.width, height));
        GUI.Box(new Rect(0, 0, Screen.width, height), "Health: " + health.ToString() + "/" + maxHealth.ToString());
        GUI.Label(new Rect(Screen.width/2 - 50, 20, 100, 35), "Time: " + text, centeredStyle);
        GUI.Label(new Rect(Screen.width/2 - 50, 40, 100, 35), "Money: " + money.ToString(), centeredStyle);
        GUI.EndGroup();
    }

    private string GetTimeString()
    {
        //Return the appropriate string given the past bool
        string text = "100 BD";
        if (!past) text = "75 AD";
        return text;
    }
}
