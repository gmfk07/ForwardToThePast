using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int health;
    public float height;
    public bool past;
    public bool invincible;
    public float invincibleTimer;
    public float maxInvincibleTimer;
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
    }

    public void HurtPlayer(int damage = 1) {
        if (!invincible)
        {
            health-= 1;
            //TODO: Player dies when health reaches 0
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

    private void OnGUI()
    {
        string text = GetTimeString();

        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        //Make the HUD
        GUI.BeginGroup(new Rect(0, 0, Screen.width, height));
        GUI.Box(new Rect(0, 0, Screen.width, height), "Health: " + health.ToString());
        GUI.Label(new Rect(Screen.width/2-50, 20, 100, 35), "Time:" + text, centeredStyle);
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
