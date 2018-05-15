using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour {
    private List<int> hitList;

    void Start () {
        Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    private void Awake()
    {
        hitList = new List<int>();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        int colliderId = collider.GetInstanceID();
        if(collider.gameObject.layer == 9)
        {
            Destroy(collider.gameObject);
        }
        if ((collider.gameObject.layer == 8 || collider.gameObject.layer == 10) && !hitList.Contains(colliderId))
        {
            collider.gameObject.GetComponent<EnemyBasic>().TakeDamage();
            hitList.Add(colliderId);
        }
        
    }
}