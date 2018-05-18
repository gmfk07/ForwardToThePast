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
    public float spawnxPast;
    public float spawnyPast;
    public float spawnxFuture;
    public float spawnyFuture;
    public float knockbackForce = 6f;
    public AudioClip deathSound;
    public AudioClip hurtSound;
    private AudioSource playerAudioSource;
    private Renderer playerRenderer;
    private Rigidbody2D playerRigidBody;

    private void Start()
    {
        playerRenderer = GetComponent<Renderer>();
        playerRigidBody = GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
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
            if (past)
                transform.position = new Vector3(spawnxPast, spawnyPast, transform.position.z);
            else
                transform.position = new Vector3(spawnxFuture, spawnyFuture, transform.position.z);
            health = maxHealth;
            money = 0;
            PlaySound(deathSound);
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

    private void PlaySound(AudioClip clip)
    {
        playerAudioSource.clip = clip;
        playerAudioSource.Play();
    }

    public void HurtPlayer(int damage = 1) {
        if (!invincible)
        {
            PlaySound(hurtSound);
            health -= 1;
            invincible = true;
            StartTimer();
        }
    }

    //When Also Hurt By Enemy
    public void KnockbackPlayer(Vector3 source, float customForce = -1) {
        customForce = (customForce == -1 ? knockbackForce : customForce);
        Vector2 dir = (transform.position - source).normalized;
        playerRigidBody.AddForce(dir * customForce, ForceMode2D.Impulse);
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
