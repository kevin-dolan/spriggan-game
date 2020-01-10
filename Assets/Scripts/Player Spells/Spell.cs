using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    /* Initially created by Kevin Dolan on 10/15.
     * This script handles spell behavior on blocks and checks if the spells should be cast on the selected blocks at all. 
     * This script should be attached to the Game Manager object.
     */

    //all spells return a bool which indicates whether the attempt at casting the spell was valid or not. if not valid, don't decrement from the number of spell uses left for that spell.
    [SerializeField] private GameObject emptyBlock; //empty block prefab 
    [SerializeField] private GameObject iceBlock; //ice block prefab
    [SerializeField] private GameObject windBlock; //windthrow block prefab

    void Start()
    {

    }

    //fireblast spell which destroys a normal solid block or an ice block. takes a GameObject which gets destroyed if the tag is either "Block" or "IceBlock".
    public bool FireBlast(GameObject oldBlock)
    {
        if(oldBlock.tag == "Wood" || oldBlock.tag == "IceBlock") //if solid dirt block or ice block
        {
            Instantiate(emptyBlock, oldBlock.transform.position, Quaternion.identity); //instantiate an empty space block in the space of the old block
            Destroy(oldBlock); //destory the old block
            return true; //was a valid target, take away a spell use
        }
        else
        {
            return false; //was not a valid target, don't take away a spell use
        }
    }

    //icecreate spell which creates an ice block instance. takes a GameObject which gets destroyed if the tag is "emptyBlock".
    public bool IceCreate(GameObject oldBlock)
    {
        if (oldBlock.tag == "emptyBlock") //if empty block
        {
            Instantiate(iceBlock, oldBlock.transform.position, Quaternion.identity); //instantiate an iceblock in the space of the empty block
            
            Destroy(oldBlock); //delete the empty space block
            return true; //was a valid target, take away a spell use
        }
        else
        {
            return false; //was not a valid target, don't take away a spell use
        }
    }

    //you may be wondering where the actual upward force is applied. that code is inside Spriggan.cs inside "OnTriggerEnter2D", switch case "Tornado".
    public bool WindThrow(GameObject oldBlock)
    {
        if (oldBlock.tag == "emptyBlock") //if empty block
        {
            Instantiate(windBlock, oldBlock.transform.position, Quaternion.identity); //instantiate a wind block in the space of the empty block

            Destroy(oldBlock); //delete the empty space block
            return true; //was a valid target, take away a spell use
        }
        else
        {
            return false; //was not a valid target, don't take away a spell use
        }
    }
}
