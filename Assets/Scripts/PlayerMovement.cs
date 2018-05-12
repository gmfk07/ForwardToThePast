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
        //Movement
        float vertInput = Input.GetAxis("Vertical");
        float horInput = Input.GetAxis("Horizontal");
        transform.position += new Vector3(horInput * speed, vertInput * speed);
        
        //Attack
        Direction? attackDirection = GetAttackDirection();
        if (canAttack && attackDirection != null)
            Attack(attackDirection);

        //Talk
        if (Input.GetButtonDown("Talk"))
        {
            NPC npc = FindTalkableNpc();
            if (npc != null)
                npc.Talk();
        }
    }

    //Attempts to find an npc that is within its talk range
    private NPC FindTalkableNpc()
    {
        NPC talkable = null;
        float closestDist = float.MaxValue;
        NPC[] npcs = GameObject.FindObjectsOfType<NPC>();
        foreach (NPC npc in npcs)
        {
            Vector2 toNpc = transform.position - npc.transform.position;
            float dist = toNpc.magnitude;
            if (dist < npc.interactRadius && dist < closestDist)
            {
                closestDist = dist;
                talkable = npc;
            }
        }
        return talkable;
    }

    //Attack Functions
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
