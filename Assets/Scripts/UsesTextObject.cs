using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsesTextObject : MonoBehaviour
{
    private string name;
    private int numOfUses;

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public int NumOfUses
    {
        get { return numOfUses; }
        set
        {
            numOfUses = value;
            gameObject.GetComponent<Text>().text = numOfUses + "";
        }
    }
}
