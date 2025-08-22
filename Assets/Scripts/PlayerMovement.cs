using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour

{

    public float speedy;
    public float jumpy;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    public int lives;
    public Vector3 posSave;
    public int coins;
    public TMP_Text cointxt;
    public Sprite sIdle;
    public Sprite sJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        posSave = transform.position;
        cointxt.text = "Coins: " + coins;
    }
    void Jump()
    {
        Vector2 verticalMovement = new Vector2(0, jumpy);
        rb.AddForce(verticalMovement);
    }

    bool CheckGround()
    {
        float rayLength = 1.7f;
        LayerMask mask = LayerMask.GetMask("obstruction");
        RaycastHit2D stopWall = Physics2D.Raycast(transform.position, -transform.up, rayLength, mask);
        Vector3 start = transform.position;
        Vector3 dir = -transform.up * rayLength;
        //Debug.DrawRay(start, dir, Color.white, 2);
        if (stopWall.collider != null)
        {
            return true;

        }
        else
        {
            return false;
        }
    }

    void TryToJump()
    {
        if (CheckGround() == true)
        {
            Vector2 verticalMovement = new Vector2(0, jumpy);
            rb.AddForce(verticalMovement);
        }
    }
    void DieLoser()
    {
        // show you died
        print("man im dead");
        SceneManager.LoadScene("you died fricken loser");
        lives -= 1;
        //update lives text
        //respawn
        //teleport "back" (posSave)
        transform.position = posSave;
        //put stuff back the way it was
    }



    // Update is called once per frame
    void Update()
    {
        Vector2 horizontalMovement = new Vector2(speedy * Time.deltaTime, 0);
        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            transform.Translate(horizontalMovement);
            rend.flipX = false;
        }

        if (Input.GetKey("a") || Input.GetKey("left"))
        {
            transform.Translate(-horizontalMovement);
            rend.flipX = true;
        }


        if ((Input.GetKeyDown("w") || Input.GetKeyDown("space") || Input.GetKeyDown("up")))
        {
            TryToJump();
            rend.sprite = sJump;
        }

        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            //    rb.AddForce(-verticalMovement);
        }
        transform.rotation = Quaternion.identity;

        if (CheckGround() == true)
        {
            rend.sprite = sIdle;
        }
        else
        {
            rend.sprite = sJump;
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.tag == "meanyPants")
        {
            ContactPoint2D point = collision.GetContact(0);
            print("Me: " + transform.position.y);
            print("Them: " + point.point.y);
            if (transform.position.y - 1f > point.point.y && rb.velocity.y <= 0)
            {
                Destroy(collision.gameObject);
                Jump();
            }
            else
            {
                DieLoser();
            }
        }
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "DeathTrigger")
        {
            DieLoser();
        }

        if (other.gameObject.tag == "Collectible")
        {
            coins += 1;
            Destroy(other.gameObject);
            cointxt.text = "Coins: " + coins;
        }
    }
    
}