using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GopherEnemy : MonoBehaviour {

    SpriteRenderer render;

    public float popTimer = 1f;
    public float hideTimer = 0.5f;
    public float shootRange = 6.5f;
    public float projectileSpeed = 4f;
    public float projectileTimer = 2f;
    public Sprite popSprite;
    public Sprite hideSprite;
    private bool hidden = true;
    public GameObject projectileObject;
    public bool smartTargeting = false;
    private GameObject player;
    private bool shooting = false;
    public float customAngle;
    Animator animator;

    private void Start()
    {
        player = GameObject.Find("Player");
        StartCoroutine(Pop());
        render = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    private void Update()
    {
        Vector3 direction = player.transform.position - transform.position;
        if (direction.magnitude <= shootRange && !shooting)
            shooting = true;
        if (direction.magnitude > shootRange && shooting)
            shooting = false;

        if (hidden)
        {
            render.sprite = hideSprite;
            gameObject.layer = 0;
        }
        else
        {
            render.sprite = popSprite;
            gameObject.layer = 8;
        }
    }

    IEnumerator Pop()
    {
        while (true)
        {
            yield return new WaitForSeconds(popTimer);
            if (shooting)
            {
                hidden = false;
                var created = Instantiate(projectileObject);
                created.transform.position = transform.position;
                float angle;
                if (!smartTargeting)
                {
                    //angle = Random.Range(0, 2 * Mathf.PI);
                    angle = customAngle*Mathf.PI/180;
                    created.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(angle) * projectileSpeed, Mathf.Cos(angle) * projectileSpeed));
                }
                else
                {
                    Vector3 dir = player.transform.position - transform.position;
                    created.GetComponent<Rigidbody2D>().AddForce(dir.normalized * projectileSpeed);
                }
                created.GetComponent<Projectile>().timer = projectileTimer;
                animator.SetBool("Popped", true);
                yield return new WaitForSeconds(hideTimer);
                animator.SetBool("Popped", false);
                hidden = true;
            }
        }
    }
}
