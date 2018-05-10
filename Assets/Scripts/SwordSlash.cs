using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlash : MonoBehaviour {
    private List<Collider2D> hitList;

    void Start () {
        Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Enemy" && !hitList.Contains(collider))
        {
            collider.gameObject.GetComponent<EnemyBasic>().Damage();
            hitList.Add(collider);
        }
    }
}