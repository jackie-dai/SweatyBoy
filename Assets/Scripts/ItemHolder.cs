using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

    [SerializeField]
    private Sprite itemHeld1;
    [SerializeField]
    private Sprite itemHeld2;
    [SerializeField]
    private Sprite itemHeld3;
    [SerializeField]
    private string itemType;

    private Sprite[] sprites;
    private int holdingItem = 0;

    [SerializeField]
    private Transform door;
    private Door closedDoor;

    void Awake()
    {
        Sprite[] temp = { itemHeld1, itemHeld2, itemHeld3 };
        sprites = temp;
        closedDoor = door.GetComponent<Door>();
        if (closedDoor == null)
        {
            Debug.Log("ERROR: Did not link door to itemslot");
        }
    }

    public bool HandleItem(GameObject item)
    {
        if (item.transform.CompareTag(itemType) && holdingItem < 3)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[holdingItem];
            holdingItem += 1;
            return false;
        }
        return true;
    }

    void Update()
    {
        //Debug.Log("Items held: " + holdingItem);
        if (holdingItem >= 3)
        {
            //Debug.Log("Door is open");
            closedDoor.OpenDoor();
        }
    } 
}
