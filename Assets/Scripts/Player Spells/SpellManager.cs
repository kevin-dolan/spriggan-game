using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/* Initially created by Kevin Dolan on 10/15.
 * This script checks for what spell the player is selecting, what block the player is selecting to cast the spell on, and then calling on the Spells script methods to actually perform the spells.
 * The script takes in a ScriptableObject called LevelSpells which keeps track of the what and how many spells may be cast on a level.
 * This script should be attached to the Game Manager object. 
 */



public class SpellManager : MonoBehaviour
{
    public List<int> usableSpells = new List<int>(); //list which holds on to local copy of the values in the LevelSpells serialized object
    private Spell spell; //instance of spell class. used to cast spells (methods) on selected blocks.
    private SpellType? selectedSpell = null; //which spell the player has selected. all the "?" does at the end of "SpellType" is make it so that i can define the value of selectedSpell as null.
    private GameObject selectedBlock = null; //block the player has clicked on which will have a spell cast upon it
    public bool previewOn = false;  //bool that checks for preview block coonditions

    private GameObject selectedSpellObject = null;

    [SerializeField] private GameObject createIceIcon;
    [SerializeField] private GameObject fireBlastIcon;

    public List<GameObject> spellUsesTextList;

    [SerializeField] private GameObject iceExplosionEffect;
    [SerializeField] private GameObject fireExplosionEffect;

    //holder variables. these variables serve no purpose other than to save space in memory.
    Vector3 mousePos;
    RaycastHit2D hit;
    RaycastHit2D blockPos;

    // Start is called before the first frame update
    void Start()
    {
        usableSpells.Add(0);
        usableSpells.Add(0);
        usableSpells.Add(0);

        spell = gameObject.GetComponent<Spell>(); //get spell component

        // Resets the color of the icons to white, incase they were turned gray by running out of uses
        createIceIcon.GetComponent<SpriteRenderer>().color = Color.white;
        fireBlastIcon.GetComponent<SpriteRenderer>().color = Color.white;

    }

    // Update is called once per frame
    void Update()
    {
        //previewBlock.transform.position = Input.mousePosition;
        if(selectedSpell == null) //if player has not selected a spell yet, make them select a spell
        {
            SpellCheck(); //select a spell
        }        
        else if (selectedSpell != null && selectedBlock == null) //if player HAS selected a spell but has not selected a block to affect yet, make them select a block
        {
            BlockCheck(); //select a block
        }
        else if(selectedBlock != null) //if player has selected a spell AND a block, cast a spell on a block
        {
            CastSpell(); //cast the selected spell on the selected block
        }

        CancelCheck(); //if player right clicks, unselect both their spell and block (set them both to null)

        if(Input.GetKeyDown(KeyCode.K))
        {
            //Debug.Log("Selected Spell: " + selectedSpell); //no catch for if null
            //Debug.Log("Selected Block: " + selectedBlock.name); //no catch for if null
            Debug.Log("FireBlast Spell Uses Remaining: " + usableSpells[0]);
            Debug.Log("IceCreate Spell Uses Remaining: " + usableSpells[1]);
            Debug.Log("++++++++++++++++++++++");
        }

        // Loops through the list of spell uses text
        for(int i = 0; i < spellUsesTextList.Count; i ++)
        {
            // If the name of the text is equal to the name of the ice spell
            if (spellUsesTextList[i].name == gameObject.GetComponent<LevelCreate>().iceSpell.name + "(Clone)")
            {
                // Set its number of uses to the ice spells uses
                spellUsesTextList[i].GetComponent<UsesTextObject>().NumOfUses = usableSpells[1];
            }
            // If the name of the text is equal to the name of the fire spell
            else if (spellUsesTextList[i].name == gameObject.GetComponent<LevelCreate>().fireSpell.name + "(Clone)")
            {
                // Set its number of uses to the fire spells uses
                spellUsesTextList[i].GetComponent<UsesTextObject>().NumOfUses = usableSpells[0];
            }
            else if (spellUsesTextList[i].name == gameObject.GetComponent<LevelCreate>().windSpell.name + "(Clone)")
            {
                // Set its number of uses to the wind spells uses
                spellUsesTextList[i].GetComponent<UsesTextObject>().NumOfUses = usableSpells[2];
            }
        }
    }

    private void CastSpell()
    {
        switch(selectedSpell)
        {
            case SpellType.FireBlast:
                if(spell.FireBlast(selectedBlock)) //all spells return a bool which indicates whether the spell was cast on a valid target or not
                {
                    usableSpells[0]--; //decrement the number of uses left for this spell
                    previewOn = false;

                    //play sound and particle effect?
                    fireExplosionEffect.transform.position = gameObject.GetComponent<LevelCreate>().camera.ScreenToWorldPoint(Input.mousePosition);
                    fireExplosionEffect.GetComponent<ParticleSystem>().Play();

                    if (usableSpells[0] == 0)
                    {
                        //gray out spell icon and play error noise?
                        selectedSpellObject.GetComponent<SpriteRenderer>().color = Color.gray;
                        previewOn = false;
                    }
                }
                break;

            case SpellType.IceCreate:
                if(spell.IceCreate(selectedBlock))
                {
                    usableSpells[1]--;
                    previewOn = false;
                    //play sound and particle effect?
                    iceExplosionEffect.transform.position = gameObject.GetComponent<LevelCreate>().camera.ScreenToWorldPoint(Input.mousePosition);
                    iceExplosionEffect.GetComponent<ParticleSystem>().Play();

                    if (usableSpells[1] == 0)
                    {
                        //gray out spell icon and play error noise?
                        selectedSpellObject.GetComponent<SpriteRenderer>().color = Color.gray;
                        previewOn = false;
                    }
                }
                break;

            case SpellType.WindThrow:
                if (spell.WindThrow(selectedBlock))
                {
                    usableSpells[2]--;
                    previewOn = false;
                    //play sound and particle effect?

                    //REPLACE WITH WIND EFFECTS
                    iceExplosionEffect.transform.position = gameObject.GetComponent<LevelCreate>().camera.ScreenToWorldPoint(Input.mousePosition);
                    iceExplosionEffect.GetComponent<ParticleSystem>().Play();

                    if (usableSpells[2] == 0)
                    {
                        //gray out spell icon and play error noise?
                        selectedSpellObject.GetComponent<SpriteRenderer>().color = Color.gray;
                        previewOn = false;
                    }
                }
                break;

            default:
                //idk how you would get to this state, but don't do anything i guess
                
                break;
        }

        selectedSpell = null;
        selectedBlock = null;
        previewOn = false;
    }

    //credit to post https://kylewbanks.com/blog/unity-2d-detecting-gameobject-clicks-using-raycasts for the way to check a clicked game object
    private void SpellCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
            if (hit.collider != null)
            {
                //Debug.Log("Spell Check: " + hit.collider.gameObject.name);

                switch(hit.collider.gameObject.name) //check name of object that was just clicked
                {
                    case "Spell Icon FireBlast(Clone)":
                        if (usableSpells[0] > 0) //if there are any uses left of this spell
                        {
                            selectedSpell = SpellType.FireBlast;
                            //Debug.Log("FireBlast Spell Selected");

                            selectedSpellObject = hit.collider.gameObject;
                            previewOn = true;
                        }
                        break;

                    case "Spell Icon IceCreate(Clone)":
                        if (usableSpells[1] > 0) //if there are any uses left of this spell
                        {
                            selectedSpell = SpellType.IceCreate;
                            //Debug.Log("IceCreate Spell Selected");

                            selectedSpellObject = hit.collider.gameObject;
                            previewOn = true;
                        }
                        break;

                    case "Spell Icon WindThrow(Clone)":
                        if (usableSpells[2] > 0) //if there are any uses left of this spell
                        {
                            selectedSpell = SpellType.WindThrow;
                            //Debug.Log("IceCreate Spell Selected");

                            selectedSpellObject = hit.collider.gameObject;
                            previewOn = true;
                        }
                        break;

                    default:
                        //in case this isn't a spell button, don't do anything other than set selectedSpell to null.
                        selectedSpell = null;
                        break;
                }
            }
        }
    }


    private void BlockCheck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), Vector2.zero);
            if (hit.collider != null)
            {
                //Debug.Log("Block Check: " + hit.collider.gameObject.name);
                
                if(hit.collider.tag == "Block" || hit.collider.tag == "emptyBlock" || hit.collider.tag == "IceBlock" || hit.collider.tag == "Wood")
                {
                    selectedBlock = hit.collider.gameObject; //get reference to block clicked on
                }
            }
            else
            {
                selectedBlock = null;
                //play error noise to convey to player that they have selected in invalid block?
            }
        }
    }

    //if player ever presses the right mouse button, unselect their current spell and block
    private void CancelCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Cancelled selection.");

            //cancel selection
            selectedSpell = null;
            selectedBlock = null;

            previewOn = false;
            //play noise to convey unselection?
        }
    }

   
    public SpellType? SelectedSpell
    {
        get
        {
            return selectedSpell;
        }
    }
    
}
