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

    void Awake()
    {
        Sprite[] temp = { itemHeld1, itemHeld2, itemHeld3 };
        sprites = temp;
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
}
