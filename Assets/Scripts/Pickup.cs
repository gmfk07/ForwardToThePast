using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public int money = 0;
    public int health = 0;
    public bool artifact = false;

	void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            if (artifact)
            {
                int layerInt = gameObject.layer;
                print("this boy: " + layerInt);
                var artifactList = Physics2D.OverlapCircleAll(transform.position, 999999, layerInt);
                for (var i = 0; i < artifactList.Length; i++)
                {
                    print(i);
                    print(artifactList[i]);
                    print(artifactList[i].gameObject.layer);
                    if (artifactList[i].gameObject.layer == layerInt) 
                        Destroy(artifactList[i].gameObject);
                }
            }
            collider.gameObject.GetComponent<PlayerStats>().money += money;
            collider.gameObject.GetComponent<PlayerStats>().IncreaseHealth(health);
            Destroy(gameObject);
        }
    }
}
