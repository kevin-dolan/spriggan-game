using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    //Created by Kevin Dolan on 10/15? 10/14? I can't remember.
    //This script handles removing spriggans from the level when they collide with the cage prefab. 
    //This script's important part is called with a gameobject tagged "Spriggan" collides with the game object it's attached to (the cage prefab, in case it wasn't clear).

    private int spriggansCaptured = 0; //number of spriggans captured in this cage. incremented whenever a spriggan collides with the cage.
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<LevelManager>(); //get a reference to 
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.transform.tag == "Spriggan") //if the object collided with is tagged as "Spriggan", increment spriggansCaptured and delete the spriggan.
        {
            Debug.Log("Spriggan captured!");
            spriggansCaptured++;
            levelManager.SprigganCaptured(col.transform.gameObject); //activate the LevelManager's SprigganCaptured method so that the win condition can be checked.
            Destroy(col.gameObject); //delete the captured spriggan. this line will probably need to be changed later to use either the spriggan's own self-delete method or a method in the game manager.
        }
    }

    //public getter for spriggansCaptured
    public int SpriggansCaptured
    {
        get
        {
            return spriggansCaptured;
        }
    }
}
