using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class NPC1 : MonoBehaviour
{
    public TMP_Text dialogueText;
    private Queue<string> sentences;

    public Dialogue1 dialogue;
    public GameObject pl;
    public GameObject npc1;
    public GameObject npc2;
    private NPC2 npc2Script;
    public Rigidbody2D playerRB;
    private float distance1;
    private bool conv1=false;

    void Start()
    {
        sentences=new Queue<string>();
        distance1=Vector2.Distance(npc1.transform.position,pl.transform.position);
        dialogueText.enabled=false;
        npc2Script=npc2.GetComponent<NPC2>();
    }

    void Update()
    {
        distance1=Vector2.Distance(npc1.transform.position,pl.transform.position);
        if(distance1<=0.5f&&Input.GetKeyDown("j")&&!conv1){
            playerRB.constraints=RigidbodyConstraints2D.FreezePosition;
            conv1=true;
            StartDialogue(dialogue);
        }
        if(conv1){
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
            if(npc2Script.conv2==false){
                playerRB.constraints=RigidbodyConstraints2D.None;
                playerRB.constraints=RigidbodyConstraints2D.FreezeRotation;
            }
            dialogueText.enabled=false;
            return;
        }
        string sentence=sentences.Dequeue();
        dialogueText.text=sentence; 
    }
}
