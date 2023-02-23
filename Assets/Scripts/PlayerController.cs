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
    #region Animation Variables
    private Animator animationController;
    private bool facing_right;
    public bool chased;
    #endregion
    #endregion

    #region Code
    void Awake()
    {
        movementSpeed = defaultMovementSpeed;
        animationController = GetComponent<Animator>();
        facing_right = false;
        chased = false;
    }
    void Update()
    {
        ProcessMovement();
        ProcessKeys();
        animationController.SetBool("Chased", chased);
    }

    void ProcessMovement()
    {
        /* Handles sprint */
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        UnityEngine.Vector3 direction = new UnityEngine.Vector3(horizontalinput, verticalInput, 0);
        if (direction != UnityEngine.Vector3.zero)
        {
            currDirection = direction;
            animationController.SetBool("Moving", true);
            if (Input.GetKey(sprintBtn))
            {
                movementSpeed = defaultSprintSpeed;
                animationController.SetBool("Running", true);
            }
            else
            {
                movementSpeed = defaultMovementSpeed;
                animationController.SetBool("Running", false);
            }
        } else
        {
            animationController.SetBool("Moving", false);
        }
        animationController.SetFloat("Xdir", direction.x);
        animationController.SetFloat("Ydir", direction.y);
        if(facing_right == false && direction.x > 0)
        {
            facing_right = true;
            UnityEngine.Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        } else if(facing_right == true && direction.x <= 0)
        {
            facing_right = false;
            UnityEngine.Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
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
                    UnityEngine.Vector3 temp = currDirection.normalized * 0.5f;
                    Object.Instantiate(item1, transform.position + temp, UnityEngine.Quaternion.identity);
                    holding_item1 = false;
                    
                }
            }
        }
    }
}
    #endregion