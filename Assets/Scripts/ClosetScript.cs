using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosetScript : MonoBehaviour
{
    private GameObject player_store;
    // Start is called before the first frame update
    void Start()
    {
        player_store = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(player_store != null && Input.GetKeyDown(KeyCode.G))
        {
            player_store.SetActive(true);
            player_store = null;
        }
    }

    public void SetPlayerStore(GameObject p)
    {
        player_store = p;
    }
}
