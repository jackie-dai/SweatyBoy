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
    #endregion 

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;   
        otherWaypoint = transform.Find("DestinationWaypoint").transform.position;
        currentDestination = otherWaypoint;
        animationController = GetComponent<Animator>();
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
    }

    void ChasePlayer()
    {
        /* Enemy speeds up the longer in chase mode */
        if (elapsedChaseTime > timeBeforeSpeedUp && movementSpeed < maxChaseMovementSpeed)
        {
            movementSpeed += 0.1f;
        }

        Vector3 directionToPlayer = playerPosition.position - transform.position;
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
            Vector3 directionToWaypoint = currentDestination - transform.position;
            rb.velocity = directionToWaypoint.normalized * movementSpeed;
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
