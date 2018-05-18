using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

    #region Singleton
    public static DialogueManager instance;
    void Awake()
    {
        instance = this;
    }
    #endregion Singleton

    public Text nameText;
    public Text dialogueText;
    public Animator boxAnimator;
    public float readyDelay = 0.2f;
    public delegate void OnDialogueEnd();
    public event OnDialogueEnd onDialogueEnd;

    private Queue<string> sentences;
    bool ready;
    

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButtonDown("Talk") && ready)
        {
            DisplayNextSentence();
            
            StartCoroutine(WaitForReadyDelay());
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        boxAnimator.SetBool("InDialogue", true);
        nameText.text = dialogue.name;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
        StartCoroutine(WaitForReadyDelay()); //initial wait
    }

    void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //Next
        
    }

    void EndDialogue() {
        //Debug.Log("Ending dialogue");
        boxAnimator.SetBool("InDialogue", false);
        if (onDialogueEnd != null)
            onDialogueEnd();
    }

    IEnumerator WaitForReadyDelay() {
        ready = false;
        yield return new WaitForSeconds(readyDelay);
        ready = true;
    }

    public bool IsInDialogue() {
        return ready && boxAnimator.GetBool("InDialogue");
    }
}
