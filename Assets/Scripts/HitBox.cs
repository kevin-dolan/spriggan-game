using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject spriggan;
    public bool boxCollision = false;

    void Start()
    {
        spriggan = GameObject.FindWithTag("Spriggan");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("Collision detected with " + collision.gameObject.tag);
        switch (collision.gameObject.tag)
        {
            case "Block":
                boxCollision = true;
                break;
            case "IceBlock":
                boxCollision = true;
                break;
            case "Wood":
                boxCollision = true;
                break;
            case "emptyBlock":
                boxCollision = false;
                break;

        } 
    }
}
