using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed=5f;
    public float jumpForce=200f;
    public Rigidbody2D playerRB;
    private float dirX;
    private int playerLayer,platformLayer;
    private bool jumpOffCoroutineRunning=false;
    // Start is called before the first frame update
    void Start()
    {

        playerLayer=LayerMask.NameToLayer("Player");
        platformLayer=LayerMask.NameToLayer("Platform");
    }

    // Update is called once per frame
    void Update()
    {
        dirX=Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown("k")&&!Input.GetKey("s")&&playerRB.velocity.y<=0.01&&playerRB.velocity.y>=-0.01){
            playerRB.AddForce(Vector2.up*jumpForce,ForceMode2D.Force);
        }
        else if(Input.GetKeyDown("k")&&Input.GetKey("s")){
            StartCoroutine("JumpOff");
        }
        if(playerRB.velocity.y>0){
            Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,true);
        }
        else if(playerRB.velocity.y<=0&&!jumpOffCoroutineRunning){
            Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,false);
        }
    }

    private void FixedUpdate()
    {
        playerRB.velocity=new Vector2(dirX*speed,playerRB.velocity.y);
    }

    IEnumerator JumpOff(){
        jumpOffCoroutineRunning=true;
        Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,true);
        yield return new WaitForSeconds(0.3f);
        Physics2D.IgnoreLayerCollision(playerLayer,platformLayer,false);
        jumpOffCoroutineRunning=false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="Level9"){
            SceneManager.LoadScene(0);
        }
    }
}
