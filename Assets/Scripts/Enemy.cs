using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private bool seePlayer = false;
    private Transform playerPosition;
    private Rigidbody2D rb;
    #region Movement Variables
    [SerializeField]
    private float movementSpeed = 1.5f;
    private float patrolMovementSpeed = 1.5f;
    private float maxChaseMovementSpeed = 4f;
    private float elapsedChaseTime = 0;
    private float timeBeforeSpeedUp = 2f;
    private float currentDirection;
    #endregion
    #region Patrol Variables
    private Vector3 initialPosition;
    [SerializeField]
    private float patrolDistance;
    private Vector3 otherWaypoint;
    private Vector3 currentDestination;
    private float distanceThreshold = 1f;
    #endregion
    #region Enemy Animation 
    private Animator animationController;
    private bool right_facing;
    #endregion 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;   
        otherWaypoint = transform.Find("DestinationWaypoint").transform.position;
        currentDestination = otherWaypoint;
        animationController = GetComponent<Animator>();
        right_facing = false;
    }

    void Update()
    {
        if (seePlayer)
        {
            ChasePlayer();
        } else
        {
            PatrolState();
        }
        DirectionFacing();
        HandleMovementAnimation();
    }

    private void HandleMovementAnimation()
    {
        if (rb.velocity.sqrMagnitude > 0)
        {
            animationController.SetBool("Moving", true);
        } else
        {
            animationController.SetBool("Moving", false);
        }
        if (rb.velocity.sqrMagnitude > 1)
        {
            animationController.SetBool("Running", true);
        } else
        {
            animationController.SetBool("Running", false);
        }
        if (animationController.GetCurrentAnimatorStateInfo(0).IsName("Transform"))
        {
            rb.velocity = UnityEngine.Vector3.zero;
        }
    }

    void DirectionFacing()
    {
        if (right_facing == false && rb.velocity.x > 0)
        {
            right_facing = true;
            UnityEngine.Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        else if (right_facing == true && rb.velocity.x <= 0)
        {
            right_facing = false;
            UnityEngine.Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            transform.localRotation = UnityEngine.Quaternion.Euler(0, 0, 0);
        }
    }

    void ChasePlayer()
    {
        /* Enemy speeds up the longer in chase mode */
        if (elapsedChaseTime > timeBeforeSpeedUp && movementSpeed < maxChaseMovementSpeed)
        {
            movementSpeed += 0.1f;
        }

        Vector3 directionToPlayer = playerPosition.position - transform.position;
        animationController.SetFloat("Xdir", directionToPlayer.x);
        animationController.SetFloat("Ydir", directionToPlayer.y);
        rb.velocity = directionToPlayer.normalized * movementSpeed;
        elapsedChaseTime += Time.deltaTime;
    }

    void PatrolState()
    {
        elapsedChaseTime = 0;
        movementSpeed = patrolMovementSpeed;
        /* In order to set patrol waypoints. 
         * Inside of enemy hierarchy, drag child object "DestinationWaypoint" to desired location */
        if (Vector3.Distance(transform.position, currentDestination) > distanceThreshold)
        {
            Vector3 directionToWaypoint = (currentDestination - transform.position).normalized;
            rb.velocity = directionToWaypoint * movementSpeed;
            animationController.SetFloat("Xdir", directionToWaypoint.x);
            animationController.SetFloat("Ydir", directionToWaypoint.y);
        } else
        {
            if (currentDestination == otherWaypoint)
            {
                currentDestination = initialPosition;
            } else
            {
                currentDestination = otherWaypoint;
            }
        }
    }
    
    /* HELPER FUNCTIONS */
    public void GetPlayer(Transform player)
    {
        playerPosition = player;
    }

    public void DetectPlayer()
    {
        seePlayer = true;
    }

    public void ResetDetectPlayer()
    {
        seePlayer = false;
    }
}
