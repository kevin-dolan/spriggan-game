using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePreview : MonoBehaviour
{
    /* Tucker Burke
     * Changes image and partical effect that follows the pointer on spell select*/

    // Local Variables
    public Sprite ice;      // Stores the images
    public Sprite fire;
    public Sprite wind;

    // Swaps to ice image and effect
    public void Ice()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = ice;
        Debug.Log("YES");
    }

    // Swaps to fire image and effect
    public void Fire()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = fire;
    }

    // Swaps to wind image and effect
    public void Wind()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = wind;
    }
}
