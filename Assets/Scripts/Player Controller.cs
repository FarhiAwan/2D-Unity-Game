using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private float direction = 0f;
    public float JumpSpeed = 6f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Animator playerAnimation;
    private Vector3 respawnPoint;
    public GameObject fallDetector;
    public TMP_Text scoreText;

    public HealthBar healthBar;

    private float damageCooldown = 1f;
    private float damageTimer = 0f;

   
    private bool isClimbing = false;
    public float climbSpeed = 4f;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        respawnPoint = transform.position;
        scoreText.text = "Score: " + Scoring.totalscore;
    }

    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        direction = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isClimbing)
        {
            player.gravityScale = 0f;
            player.velocity = new Vector2(direction * speed, vertical * climbSpeed);
        }
        else
        {
            player.gravityScale = 1f;

            if (direction > 0f){
                player.velocity = new Vector2(direction * speed, player.velocity.y);
                transform.localScale = new Vector2(0.7235012f, 0.7842882f);
            }
            else if (direction < 0f){
                player.velocity = new Vector2(direction * speed, player.velocity.y);
                transform.localScale = new Vector2(-0.7235012f, 0.7842882f);
            }
            else{
                player.velocity = new Vector2(0f, player.velocity.y);
            }
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround && !isClimbing){
            player.velocity = new Vector2(player.velocity.x, JumpSpeed);
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(player.velocity.x));
        playerAnimation.SetBool("OnGround", isTouchingGround);

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);

        if (damageTimer > 0f){
            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collosion)
    {
        if(collosion.tag == "FallDetector"){
            transform.position = respawnPoint;
        }
        else if (collosion.tag == "Checkpoint"){
            respawnPoint = transform.position;
        }
        else if (collosion.tag=="NextLevel"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(1);
        }
        else if(collosion.tag=="PreviousLevel"){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            SceneManager.LoadScene(0);
        }
        else if(collosion.tag == "Crystal"){
            Scoring.totalscore += 1;
            scoreText.text = "Score: " + Scoring.totalscore;
            collosion.gameObject.SetActive(false);
        }
        else if (collosion.CompareTag("Ladder")){
            isClimbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder")){
            isClimbing = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Spike"){
            if(damageTimer <= 0f){
                healthBar.Damage(0.1f);
                damageTimer = damageCooldown;
            }
        }
    }
}