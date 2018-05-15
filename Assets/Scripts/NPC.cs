
using UnityEngine;

public class NPC : MonoBehaviour {

    public Dialogue firstDialogue;
    public Dialogue dialogue;
    public float interactRadius = 2f;

    int timesInteracted = 0;

	// Use this for initialization
	void Start () {
		
	}

    //Can be overriden for custom interaction
    public virtual void Talk() {
        if (!firstDialogue.isEmpty() && timesInteracted == 0)
            DialogueManager.instance.StartDialogue(firstDialogue);
        else
            DialogueManager.instance.StartDialogue(dialogue);
        ++timesInteracted;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
