using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Movement Variables
    private float defaultMovementSpeed = 3f;
    private float defaultSprintSpeed = 5f;
    [SerializeField]
    private float movementSpeed;

    private UnityEngine.Vector3 currDirection;

    /* KEYBINDS */
    KeyCode sprintBtn = KeyCode.LeftShift;
    KeyCode crouchBtn = KeyCode.LeftControl;
    #endregion

    #region Item Variables
    [SerializeField]
    private GameObject item1;
    private bool holding_item1;
    private KeyCode keyToPickUp = KeyCode.F;
    #endregion

    #endregion

    #region Code
    void Awake()
    {
        movementSpeed = defaultMovementSpeed;
    }
    void Update()
    {
        ProcessMovement();
        ProcessKeys();
    }

    void ProcessMovement()
    {
        /* Handles sprint */
        if (Input.GetKey(sprintBtn))
        {
            movementSpeed = defaultSprintSpeed;
        } else
        {
            movementSpeed = defaultMovementSpeed;
        }

        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontalinput, verticalInput, 0);
        if (direction != UnityEngine.Vector3.zero)
        {
            currDirection = direction;
        }

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }

    void ProcessKeys()
    {
        if (Input.GetKeyDown(keyToPickUp))
        {
            if (holding_item1 == false)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, currDirection.normalized, 1);
                //Debug.Log("Pressed F " + currDirection + " " + hit.transform.name);
                foreach (RaycastHit2D hit in hits)
                {
                    if (hit.transform.CompareTag("Item1"))
                    {
                        holding_item1 = true;
                        Destroy(hit.transform.gameObject);
                        return;
                    }
                }
            } else
            {
                if (holding_item1)
                {
                    RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, currDirection.normalized, 1);
                    foreach (RaycastHit2D hit in hits)
                    {
                        if (hit.transform.CompareTag("ItemSlot"))
                        {
                            holding_item1 = hit.transform.GetComponent<ItemHolder>().HandleItem(item1);
                            Debug.Log("You placed an item " + !holding_item1);
                            return;
                        }
                    }
                    Object.Instantiate(item1, transform.position + currDirection.normalized, UnityEngine.Quaternion.identity);
                    holding_item1 = false;
                    
                }
            }
        }
    }
    #endregion
}
