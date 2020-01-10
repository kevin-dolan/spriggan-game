using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject[] spriggans; //array of spriggans used to define spriggansLeft
    public GameObject winThingy;
    public int spriggansLeft; //value that is reduced whenever a spriggan is captured by a cage. when this reaches zero, call level end method.
    public int spriggansLost;
    public bool startNow = false;
    public GameObject previewBlock; //OG object
    private GameObject preClone; // Clone object
    [SerializeField] private PauseMenuManager pauseMenuMngr;

    public SpellManager spellMngr;

    // Start is called before the first frame update
    void Start()
    {
        preClone = Instantiate(previewBlock, new Vector3(0, 0, 0), Quaternion.identity); //instantiate clone
        pauseMenuMngr = GameObject.FindGameObjectWithTag("gameManager").GetComponent<PauseMenuManager>(); //get reference to pause menu manager

        Invoke("DelayedStart", 1);


        spellMngr = GameObject.FindGameObjectWithTag("gameManager").GetComponent<SpellManager>();
    }

    //hacky solution. maybe move components in different order on game object?? idk, i'll figure it out tomorrow.
    private void DelayedStart()
    {
        spriggans = GameObject.FindGameObjectsWithTag("Spriggan");
        spriggansLeft = spriggans.Length;
        spriggansLost = 0;
        winThingy = GameObject.FindGameObjectWithTag("WinThingy");
       
        

        startNow = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(startNow)
        {
            if (spriggansLeft == 0)
            {
                //game win
                pauseMenuMngr.ShowWinMenu(); //show win menu when all spriggans are captured
            }
            if(spriggansLost == spriggans.Length)
            {
                pauseMenuMngr.ShowLoseMenu();
            }

            for (int i = 0; i < spriggans.Length; i++)//keep track of sprigs that fall
            {
                if (spriggans[i] != null && spriggans[i].transform.position.y < -10)
                {
                    spriggansLost++;
                    Destroy(spriggans[i]);
                }
            }
        }

        if(gameObject.GetComponent<SpellManager>().previewOn == true)
        {
            switch(spellMngr.SelectedSpell)
            {
                case SpellType.IceCreate:
                    preClone.transform.GetComponent<ChangePreview>().Ice();
                    preClone.transform.Find("IceParticles").GetComponent<Renderer>().enabled = true;
                    break;
                case SpellType.FireBlast:
                    preClone.transform.GetComponent<ChangePreview>().Fire();
                    //preClone.transform.Find("FireParticles").GetComponent<Renderer>().enabled = true;
                    break;
                case SpellType.WindThrow:
                    preClone.transform.GetComponent<ChangePreview>().Wind();
                    //preClone.transform.Find("WindParticles").GetComponent<Renderer>().enabled = true;
                    break;
            }
            preClone.GetComponent<Renderer>().enabled = true;

            
        }
        else
        {
            preClone.GetComponent<Renderer>().enabled = false;
            preClone.transform.Find("IceParticles").GetComponent<Renderer>().enabled = false;
            //preClone.transform.Find("FireParticles").GetComponent<Renderer>().enabled = false;
        }
        //Debug.Log(gameObject.GetComponent<SpellManager>().previewOn);
        //Debug.Log("Sprite Renderer is " + previewBlock.GetComponent<Renderer>().enabled);
    }

    public void SprigganAdd(GameObject spriggan)
    {
        //spriggans.Add
    }

    public void SprigganCaptured(GameObject spriggan)
    {
        spriggansLeft--;
        for(int i = 0; i < spriggans.Length; i ++)
        {
            if(spriggans[i] == spriggan)
            {
                spriggans[i] = null;
            }
        }
    }

    //this method resets the level. technically, it just reloads the current scene. i really don't like reseting the level this way, but the less lazy way of reseting a level would require a somewhat substantial amount of code refactoring in the LevelCreate script, which is not really worth the energy for so small a game. but in a larger project with many assets, this would be a really, really stupid way to reseting the level.
    public void ResetLevel()
    {
        // Resets the time back to normal so if you reset while in fast speed, you don't start in fast speed
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads current scene and effectively resets level.
    }
}
