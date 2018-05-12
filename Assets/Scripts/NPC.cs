
using UnityEngine;

public class NPC : MonoBehaviour {

    public string[] texts;
    public float interactRadius = 2f;


	// Use this for initialization
	void Start () {
		
	}

    //Can be overriden for custom interaction
    public virtual void Talk() {
        Debug.Log("Talk, talk, talk.");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}
