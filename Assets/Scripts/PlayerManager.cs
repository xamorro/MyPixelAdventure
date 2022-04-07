using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {
    private GameObject gameManager;
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D col;
    private GameObject sndManager;
    [SerializeField] private LayerMask jumpableGround;

    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpSpeed = 7f;
    private float dirX;

    private int lifes = 3;
    private bool isDead;
    private bool isPlayerReady;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        sndManager = GameObject.FindGameObjectWithTag("SoundManager");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        initPlayer();
    }

    void initPlayer() {
        isPlayerReady = isDead = false;
        lifes = 3;
        rb.bodyType = RigidbodyType2D.Dynamic;

        Invoke("setPlayerReady", 1f);
    }

    void setPlayerReady() {
        isPlayerReady = true;
    }

    void Update() {
        UpdateMovement();
        UpdateAnimator();

    }

    void UpdateMovement() {
        if (isPlayerReady) {
            //GetAxis
            dirX = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);
            
            if (Input.GetKeyDown("space") && isGrounded()) {
                sndManager.GetComponent<SoundManager>().playFX(0);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,jumpSpeed);
            }

            if ((transform.position.y < -12f) && !isDead){
                killPlayer();
            }
        }
    }

    void UpdateAnimator() {
        if (dirX > 0f) {
            anim.SetBool("run", true);
            transform.localScale = new Vector3(1, 1, 1);
        } else if (dirX < 0f){
            anim.SetBool("run", true);
            transform.localScale = new Vector3(-1, 1, 1);
        } else {
            anim.SetBool("run", false);
        }

        if (rb.velocity.y > .1f) {
            anim.SetBool("jump", true);
            anim.SetBool("fall", false);
        } else if (rb.velocity.y < -.1f) {
            anim.SetBool("jump", false);
            anim.SetBool("fall", true);
        } else {
            anim.SetBool("jump", false);
            anim.SetBool("fall", false);
        }
    }

    bool isGrounded() {
        //return (rb.velocity.y == 0f)?true:false;
        /*
        if (rb.velocity.y == 0f) 
        {
            return true;
        } else {
            return false;
        }
        */
        return Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.CompareTag("Trap") || col.gameObject.CompareTag("Enemy")) {
            killPlayer();
        }
    }

    void killPlayer() {
        isDead = true;
        isPlayerReady = false;
        sndManager.GetComponent<SoundManager>().playFX(3);
        anim.SetTrigger("dead");
        lifes -= 1;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.CompareTag("Finish"))  {
            isPlayerReady = false;
            sndManager.GetComponent<SoundManager>().playFX(2);
            rb.bodyType = RigidbodyType2D.Static;
            Invoke("CompleteLevel", 2f);
        }
    }

    void CompleteLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
