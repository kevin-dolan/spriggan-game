using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spriggan : MonoBehaviour
{
    //public float gravity;
    public float walkingSpeed = 0.075f;
    public float flipBox = -0.4f;
    public float moveUp = 0.0f;
    public bool onGround;
    public bool boxCollision = false;
    public Vector3 move;
    public GameObject sideBox;
    public GameObject topBox;
    public SpriteRenderer[] spriteRenderer;

    //added by kevin dolan
    private PauseMenuManager pauseManager;
    private Rigidbody2D rb;
    private Vector3 pausedVelocity;
    private float pausedAngularVelocity;
    private bool wasPaused = true;
    private bool wasNotPaused = false;

    bool hasJumped;
    float timer = 0.0f;

    public Vector2 windStrength = new Vector2(0.0f, 500.0f);
    //https://stackoverflow.com/questions/32324452/how-to-pause-a-ridigbody2d-in-unity

    // Start is called before the first frame update
    void Start()
    {
        onGround = false;
        //sideBox = GameObject.FindWithTag("Hitbox");
        //topBox = GameObject.FindWithTag("Topbox");
        topBox = gameObject.transform.GetChild(0).gameObject;
        sideBox = gameObject.transform.GetChild(1).gameObject;
        spriteRenderer = GetComponentsInChildren<SpriteRenderer>();

        //added by kevin dolan for pause effect
        pauseManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<PauseMenuManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pauseManager.isPaused) //if game is paused, do nothing but call the pause me method
        {
            if(wasNotPaused) //only call this once if paused, not over and over while paused
            {
                PauseMe();
            }
            
        }
        else //if game is not paused, do everything
        {
            if (wasPaused) //only call this once if unpaused, not over and over while unpaused
            {
                UnpauseMe();
            }

            if (sideBox.GetComponent<HitBox>().boxCollision == true && topBox.GetComponent<HitBox>().boxCollision == true)//if both boxes are colliding
            {
                walkingSpeed = -walkingSpeed; //invert the speed to change direction
                sideBox.transform.localPosition = new Vector2(-sideBox.transform.localPosition.x, sideBox.transform.localPosition.y); //change the boxes to be on the other side
                topBox.transform.localPosition = new Vector2(-topBox.transform.localPosition.x, topBox.transform.localPosition.y);
                sideBox.GetComponent<HitBox>().boxCollision = false;   //set collisions to false to reset the checks
                topBox.GetComponent<HitBox>().boxCollision = false;
                flipBox = -flipBox; //reset box postion float
                spriteRenderer[0].flipX ^= true;
            }

            if (sideBox.GetComponent<HitBox>().boxCollision == true && topBox.GetComponent<HitBox>().boxCollision == false)//if only bottom box colliding
            {
                if (hasJumped == false)
                {
                    moveUp = 1.0f;
                    move = new Vector3(0.0f, moveUp, 0.0f); //apply a force to get sprig over the block
                    gameObject.transform.Translate(move);
                    hasJumped = true;
                    Debug.Log("Jump");
                }
                //Debug.Log(move);
            }

            move = new Vector3(walkingSpeed, 0.0f, 0.0f) * Time.deltaTime;
            gameObject.transform.Translate(move);

            moveUp = 0.0f;

            timer += Time.deltaTime;

            if (timer >= 0.5f)
            {
                hasJumped = false;
                timer = 0.0f;
            }
        }

        //collision handled in the hitboxes

        //this collision thing isn't ready yet
        //void OnCollisionEnter2D(Collision2D collision)
        //{
        //    Collider2D collider = collision.collider;

        //    if (collider.tag == "emptyBlock")
        //    {
        //        Vector3 contactPoint = collision.contacts[0].point;
        //        Vector3 center = collider.bounds.center;

        //        bool right = contactPoint.x > center.x;
        //        bool top = contactPoint.y > center.y;
        //    }
        //}

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Tornado":
                    gameObject.GetComponent<Rigidbody2D>().AddForce(windStrength);
                break;
        }
    }


    private void PauseMe()
    {
        pausedVelocity = rb.velocity;
        pausedAngularVelocity = rb.angularVelocity;
        rb.bodyType = RigidbodyType2D.Static; //stop the rigidbody from moving

        //janky janky janky janky
        wasPaused = true;
        wasNotPaused = false;
    }

    private void UnpauseMe()
    {

        rb.bodyType = RigidbodyType2D.Dynamic; //start the rigidbody moving
        rb.velocity = pausedVelocity;
        rb.angularVelocity = pausedAngularVelocity;

        wasPaused = false;
        wasNotPaused = true;
    }
}
