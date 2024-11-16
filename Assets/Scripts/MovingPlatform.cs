using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public bool isMovingPlatform = false;
    public GameObject myPlayer;

    // Update is called once per frame
    void Update()
    {
        if(isMovingPlatform)
        {
            myPlayer.transform.SetParent(this.transform);
        }
        else
        {
            myPlayer.transform.SetParent(null);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            isMovingPlatform = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            isMovingPlatform = false;
        }
    }
}
