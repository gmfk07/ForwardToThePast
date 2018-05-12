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

    private Queue<string> sentences;

	// Use this for initialization
	void Start () {
        sentences = new Queue<string>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartDialogue(Dialogue dialogue) {
        boxAnimator.SetBool("InDialogue", true);
        nameText.text = dialogue.name;
        Time.timeScale = 0.02f;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();
    }

    void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
        //Next
        if (Input.GetButtonDown("Talk"))
            DisplayNextSentence();
    }

    void EndDialogue() {
        //Debug.Log("Ending dialogue");
        boxAnimator.SetBool("InDialogue", false);
        Time.timeScale = 1;
    }
}
