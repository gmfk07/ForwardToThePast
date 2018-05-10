using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    public float attackCooldown = 0.3f;
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
        Direction? attackDirection = GetAttackDirection();

        t.position += new Vector3(horInput * speed, vertInput * speed);
        if (canAttack && attackDirection != null)
            Attack(attackDirection);
    }

    IEnumerator AttackTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void Attack(Direction? dir)
    {
        StartCoroutine(AttackTimer());
        created = Instantiate(slash, transform.position, Quaternion.Euler(new Vector3(0, 0, 90 * (int) dir)));
        created.transform.parent = transform;
    }

    //Returns direction based on input or null if nothing
    private Direction? GetAttackDirection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            return Direction.Right;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            return Direction.Left;

        if (Input.GetKeyDown(KeyCode.UpArrow))
            return Direction.Up;

        if (Input.GetKeyDown(KeyCode.DownArrow))
            return Direction.Down;
        return null;
    }
}

public enum Direction { Left, Down, Right, Up }
