using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBlock : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 mousePos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Preview Block Location and Lerping
        mousePos = Input.mousePosition;   
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector2.Lerp(transform.position, mousePos, 10.0f);
    }
}
