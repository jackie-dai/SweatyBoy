using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Movement Variables
    private float defaultMovementSpeed = 3f;
    private float defaultSprintSpeed = 5f;
    [SerializeField]
    private float movementSpeed;

    /* KEYBINDS */
    KeyCode sprintBtn = KeyCode.LeftShift;
    KeyCode crouchBtn = KeyCode.LeftControl;
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
    }

    void ProcessMovement()
    {
        /* Handles spring */
        if (Input.GetKey(sprintBtn))
        {
            movementSpeed = defaultSprintSpeed;
        } else
        {
            movementSpeed = defaultMovementSpeed;
        }

        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalinput, verticalInput, 0);

        transform.Translate(direction * movementSpeed * Time.deltaTime);
    }
    #endregion
}
