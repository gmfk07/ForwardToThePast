using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopherEnemy : MonoBehaviour {

    public float popTimer = 1f;
    public float hideTimer = 0.5f;
    public float projectileSpeed = 4f;
    public float projectileTimer = 2f;
    private bool hidden = true;
    public GameObject projectileObject;

    private void Start()
    {
        StartCoroutine(Pop());
    }

    // Use this for initialization
    private void Update()
    {
        if (hidden)
            gameObject.layer = 0;
        else
            gameObject.layer = 8;
    }

    IEnumerator Pop()
    {
        while (true)
        {
            yield return new WaitForSeconds(popTimer);
            hidden = false;
            var created = Instantiate(projectileObject);
            created.transform.position = transform.position;
            var angle = Random.Range(0, 360);
            created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * projectileSpeed, Mathf.Cos(angle) * projectileSpeed));
            created.GetComponent<Projectile>().timer = projectileTimer;
            yield return new WaitForSeconds(hideTimer);
            hidden = true;
        }
    }
}
