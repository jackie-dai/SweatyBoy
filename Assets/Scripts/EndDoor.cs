using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDoor : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("YEcolodedE");
        if (other.tag == "Player")
        {
            Debug.Log("YES INSIDE");
            GameManager gm = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
            gm.WinGame();
        }
    }
}
