using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : NPC {

    public GameObject dispenseObject;
    public float dispenseSpeed;
    public int cost;
    public string text;
    public float yOffset = 32f;
    public bool oneOff = false;

    public override void Talk()
    {
        var player = PlayerManager.Player;
        if (player.GetComponent<PlayerStats>().money >= cost)
        {
            player.GetComponent<PlayerStats>().money -= cost;
            var created = Instantiate(dispenseObject);
            created.transform.position = transform.position;
            Vector3 dir = player.transform.position - transform.position;
            created.GetComponent<Rigidbody2D>().AddForce(dir.normalized * dispenseSpeed);

            if (oneOff)
                Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");
        centeredStyle.alignment = TextAnchor.UpperCenter;

        var position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        var textSize = GUI.skin.label.CalcSize(new GUIContent(text));
        GUI.Label(new Rect(position.x - textSize.x/2, Screen.height - position.y - yOffset, textSize.x, textSize.y), text, centeredStyle);
    }
}
