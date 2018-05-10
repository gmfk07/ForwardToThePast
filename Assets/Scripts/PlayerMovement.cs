using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float maxTimer;
    private bool canAttack;
    public GameObject slash;
    private GameObject created;

    private void Start()
    {
        canAttack = true;
    }

    private void FixedUpdate()
    {
        Transform t = GetComponent<Transform>();
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");

        t.position += new Vector3(horInput * speed, vertInput * speed);
        if (canAttack)
            Attack();
    }

    IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(maxTimer);
        canAttack = true;
    }

    private void Attack()
    {
        bool rAttack = Input.GetKeyDown(KeyCode.RightArrow);
        bool lAttack = Input.GetKeyDown(KeyCode.LeftArrow);
        bool uAttack = Input.GetKeyDown(KeyCode.UpArrow);
        bool dAttack = Input.GetKeyDown(KeyCode.DownArrow);

        if (rAttack)
            created = Instantiate(slash, transform.position, Quaternion.Euler(new Vector3 (0, 0, 180)));
        else if (lAttack)
            created = Instantiate(slash, transform.position, Quaternion.Euler(new Vector3 (0, 0, 0)));
        else if (uAttack)
            created = Instantiate(slash, transform.position, Quaternion.Euler(new Vector3 (0, 0, 270)));
        else if (dAttack)
            created = Instantiate(slash, transform.position, Quaternion.Euler(new Vector3 (0, 0, 90)));

        if (rAttack || lAttack || uAttack || dAttack)
        {
            StartCoroutine(AttackTimer());
            created.transform.parent = transform;
        }
    }
}
