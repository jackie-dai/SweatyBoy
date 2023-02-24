using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private GameObject closedDoor;

    void Awake()
    {
        closedDoor = transform.GetChild(0).gameObject;
        closedDoor.SetActive(false);
    }
    public void OpenDoor()
    {
        closedDoor.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            OpenDoor();
        }
    }
}
