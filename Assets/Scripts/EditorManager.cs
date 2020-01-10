using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditorManager : MonoBehaviour
{
    int worldWidth;
    int worldHeight;

    public GameObject placeHolderBlock;
    public Camera camera;

    public GameObject selectedBlock;
    GameObject selectedBlockCopy;

    public GameObject solidBlock;
    public GameObject emptyBlock;
    public GameObject spriggan;

    Vector3 topLeftOfCamera;

    // Start is called before the first frame update
    void Start()
    {
        worldWidth = 32;
        worldHeight = 16;

        topLeftOfCamera = camera.ScreenToWorldPoint(new Vector3(0, camera.pixelHeight, camera.nearClipPlane));

        GenerateBlankLevel();
    }

    // Update is called once per frame
    void Update()
    {
        // If the mouse was clicked
        if (Input.GetMouseButtonDown(0))
        {
            selectedBlock = SelectBlock();
            if(selectedBlock != null)
                selectedBlockCopy = Instantiate(selectedBlock);
        }

        if(selectedBlock != null)
        {
            if(selectedBlock.name == solidBlock.name)
            {
                selectedBlockCopy.transform.position = camera.ScreenToWorldPoint(Input.mousePosition);
                selectedBlockCopy.transform.position = new Vector3(selectedBlockCopy.transform.position.x, selectedBlockCopy.transform.position.y,-1);
                selectedBlockCopy.gameObject.layer = 2;
            }
        }

        // If the mouse was clicked
        if (Input.GetMouseButtonDown(0))
        {
            PlaceSelectedBlock();
        }
    }

    public void GenerateBlankLevel()
    {
        for(int row = 0; row < worldWidth; row ++)
        {
            for(int col = 0; col < worldHeight; col ++)
            {
                // Gets an x & y position for the tile to be placed
                float xPos = ((topLeftOfCamera.x + 0.5f) + row * 1);
                float yPos = ((topLeftOfCamera.y - 0.5f) + col * -1);

                placeHolderBlock.transform.position = new Vector2(xPos, yPos);
                placeHolderBlock.layer = 8;
                Instantiate(placeHolderBlock);
            }
        }
    }

    public GameObject SelectBlock()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            // If the object clicked on is at the bottom where the reference blocks are
            if (hit.transform.position.y <= -8)
            {
                return hit.transform.gameObject;
            }
        }

        return null;
    }

    public void PlaceSelectedBlock()
    {
        // Stores the position of the mouse in world space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Casts a ray from the camera to the mouse and stores what was hit in a RaycastHit object
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);

        // If something was hit
        if (hit)
        {
            // If the object clicked on is at the bottom where the reference blocks are
            if(hit.transform.gameObject.layer == 8)
            {
                Vector3 tempPosition = hit.transform.position;
                Destroy(hit.transform.gameObject);
                selectedBlockCopy.transform.position = tempPosition;
                selectedBlockCopy.transform.gameObject.layer = 8;
                selectedBlock = null;
                selectedBlockCopy = null;
            }
        }
    }
}
