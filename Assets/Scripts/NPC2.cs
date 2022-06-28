using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPC2 : MonoBehaviour
{
    public TMP_Text dialogueText;
    private Queue<string> sentences;

    public Dialogue1 dialogue;
    public GameObject pl;
    private Player playerScript;
    public GameObject npc2;
    public Rigidbody2D playerRB;
    private float distance2;
    public bool conv2=false;

    void Start()
    {
        sentences=new Queue<string>();
        distance2=Vector2.Distance(npc2.transform.position,pl.transform.position);
        dialogueText.enabled=false;
        playerScript=pl.GetComponent<Player>();
    }

    void Update()
    {
        distance2=Vector2.Distance(npc2.transform.position,pl.transform.position);
        if(distance2<=0.5f&&Input.GetKeyDown("j")&&!conv2){
            playerRB.constraints=RigidbodyConstraints2D.FreezePosition;
            conv2=true;
            StartDialogue(dialogue);
        }
        if(conv2){
            dialogueText.enabled=true;
            if(Input.GetKeyDown("j")){
                DisplayNextSentence();
            }
        }
    }

    public void StartDialogue(Dialogue1 dialogue){
        sentences.Clear();
        foreach(string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }
    }
    public void DisplayNextSentence(){
        if(sentences.Count==0){
            playerRB.constraints=RigidbodyConstraints2D.None;
            playerRB.constraints=RigidbodyConstraints2D.FreezeRotation;
            dialogueText.enabled=false;
            playerScript.jumpForce=500f;
            return;
        }
        string sentence=sentences.Dequeue();
        dialogueText.text=sentence; 
    }
}
