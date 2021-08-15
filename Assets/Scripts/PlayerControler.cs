using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    float speed = 0.06f;

    [SerializeField]
    float jump = 200f;
    int maxHealth = 5;
    Transform myTran;
    SpriteRenderer mySprite;
    Rigidbody2D rb;
    Animator Anim;
    bool isJump;
    int playerhealth = 3;
    public int Score;
    public Transform Holder;
    TextMeshProUGUI healthtext;
    TextMeshProUGUI scoretext;
    TextMeshProUGUI gameover;
    [SerializeField]
    Transform collectedSound;
    [SerializeField]
    Transform KillEnemy;
    [SerializeField]
    Transform Death;
    [SerializeField]
    Transform GameOverSound;
    // Start is called before the first frame update
    void Start()
    {

        myTran = GetComponent<Transform>();
        mySprite = GetComponent<SpriteRenderer>();
        transform.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        Score = 0;
        isJump = false;
        playerhealth = maxHealth;
        healthtext = Holder.Find("txtHealth").GetComponent<TextMeshProUGUI>();
        scoretext = Holder.Find("textScore").GetComponent<TextMeshProUGUI>();
        gameover = Holder.Find("gameover").GetComponent<TextMeshProUGUI>();
        scoretext.text = "Score : " + Score;
        healthtext.text = playerhealth + "/" + maxHealth;
        gameover.text = "";
            }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mySprite.flipX = true;
            transform.Translate(new Vector3(-1 * speed, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(speed, 0, 0));
            mySprite.flipX = false;
        }
        // Debug.Log(Input.GetAxis("Horizontal"));
        // rb.velocity = new Vector2(speed, rb.velocity.y);
        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && isJump == false)
        {
            rb.velocity = Vector2.up * jump;
            isJump = true;
        }
        if(Mathf.Abs(rb.velocity.y) < 0.01f)
        {
            isJump = false;
        }
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
        Anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speed * Input.GetAxis("Horizontal"), rb.velocity.y);
       // Debug.Log(Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag ("Enemy"))
        {
            if(isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
                SoundOnKill(collision.transform.position);
            }
            else
            {
                playerhealth--;
                healthtext.text = playerhealth + "/" + maxHealth;
                SoundOnDeath(collision.transform.position);
            }
        }
        else if (collision.CompareTag("Gem"))
        {
            Score += 100;
            scoretext.text = ("Score: " + Score);
            SoundOn(collision.transform.position);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Cherry"))
        {
            Score += 50;
            scoretext.text = ("Score: " + Score);
            SoundOn(collision.transform.position);
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("finish"))
        {
            gameover.text = "You Win";
        }
        else if (collision.CompareTag("heart"))
        {
            if(playerhealth < maxHealth)
            {
                playerhealth++;
            }
            healthtext.text = playerhealth + "/" + maxHealth;
            SoundOn(collision.transform.position);
            Destroy(collision.gameObject);
        }

        if(playerhealth <= 0)
        {
            gameover.text = "Game Over";
        }
    }
    void SoundOn(Vector3 itemPos)
    {
       Transform obj = Instantiate(collectedSound, itemPos,new Quaternion());
        obj.gameObject.SetActive(true);
        Destroy(obj, obj.GetComponent<AudioSource>().clip.length);
    }
    void SoundOnKill(Vector3 itemPos)
    {
        Transform obj = Instantiate(KillEnemy, itemPos, new Quaternion());
        obj.gameObject.SetActive(true);
        Destroy(obj, obj.GetComponent<AudioSource>().clip.length);
    }
    void SoundOnDeath(Vector3 itemPos)
    {
        Transform obj = Instantiate(Death, itemPos, new Quaternion());
        obj.gameObject.SetActive(true);
        Destroy(obj, obj.GetComponent<AudioSource>().clip.length);
    }
    void SoundOnGameOverSound(Vector3 itemPos)
    {
        Transform obj = Instantiate(GameOverSound, itemPos, new Quaternion());
        obj.gameObject.SetActive(true);
        Destroy(obj, obj.GetComponent<AudioSource>().clip.length);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (isJump && rb.velocity.y < 0)
            {
                Destroy(collision.gameObject);
                SoundOnKill(collision.transform.position);
            }
            else
            {
                playerhealth--;
                healthtext.text = playerhealth + "/" + maxHealth;
                SoundOnDeath(collision.transform.position);
                if (playerhealth <= 0)
                {
                    gameover.text = "Game Over";
                }
            }
        }
    }
}
