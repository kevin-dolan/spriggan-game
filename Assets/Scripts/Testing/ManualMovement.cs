using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is for controlling an arbitrary game object to move around. It's purpose is to test collision between different game objects/prefabs.
//Ideally this script is just attached to a game object with a collision box and maybe a rigidbody.
//Initially authored by Kevin Dolan on 10/8/19


public class ManualMovement : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f; //controlls the speed at which the object is moved around. 1.0f by default unless changed in the editor.

    // Update is called once per frame
    void FixedUpdate()
    {
        //check for keyboard input, and if keys are pressed, move the game object.
        if(Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(new Vector3(1.0f * speed, 0.0f, 0.0f) * Time.deltaTime); //right
        }
        else if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector3(-1.0f * speed, 0.0f, 0.0f) * Time.deltaTime); //left
        }

        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector3(0.0f, 1.0f * speed, 0.0f) * Time.deltaTime); //up
        }
        else if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector3(0.0f, -1.0f * speed, 0.0f) * Time.deltaTime); //down
        }

    }
}
