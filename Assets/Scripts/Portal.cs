using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : NPC {
    public float x;
    public float y;
    public bool time;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    /*public override void Talk()
    {
        DialogueManager dm = DialogueManager.instance;
        if (!firstDialogue.isEmpty() && timesInteracted == 0)
            dm.StartDialogue(firstDialogue);
        else
            dm.StartDialogue(dialogue);
        ++timesInteracted;
        StartCoroutine(Waiter(dm));
        player.transform.position = new Vector3(x, y, 0);
        if (time)
        {
           player.GetComponent<PlayerStats>().past = !player.GetComponent<PlayerStats>().past;
        }
    }*/
    public void port()
    {
        player.transform.position = new Vector3(x, y, 0);
        if (time)
        {
            player.GetComponent<PlayerStats>().past = !player.GetComponent<PlayerStats>().past;
        }

    }
    IEnumerator Waiter(DialogueManager dm)
    {
        yield return new WaitWhile(() => dm.IsInDialogue());
    }
}
