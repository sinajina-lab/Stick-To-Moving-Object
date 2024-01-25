using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) //gravity to the left
        {
            Physics2D.gravity = new Vector2(-9.81f, 0f);
        }

        else if(Input.GetKeyDown(KeyCode.DownArrow)) //gravity down
        {
            Physics2D.gravity = new Vector2(0f, -9.81f);
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow)) //gravity up
        {
            Physics2D.gravity = new Vector2(0f, 9.81f);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow)) //gravity to the right
        {
            Physics2D.gravity = new Vector2(9.81f, 0f);
        }

    }
}
