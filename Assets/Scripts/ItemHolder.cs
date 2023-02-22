using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHolder : MonoBehaviour
{

    [SerializeField]
    private Sprite itemHeld;
    [SerializeField]
    private string itemType;

    private bool holdingItem = false;

    public bool HandleItem(GameObject item)
    {
        if (item.transform.CompareTag(itemType) && !holdingItem)
        {
            holdingItem = true;
            GetComponent<SpriteRenderer>().sprite = itemHeld;
            return false;
        }
        return true;
    }
}
