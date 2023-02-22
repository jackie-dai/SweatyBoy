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
    private float movementSpeed = 3f;
    #endregion
    #region Patrol Variables
    private Vector3 initialPosition;
    [SerializeField]
    private float patrolDistance;
    private Vector3 destinationWaypoint;
    #endregion

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;   
        destinationWaypoint = transform.Find("DestinationWaypoint").transform.position;
    }

    void Update()
    {
        if (seePlayer)
        {
            Vector3 directionToPlayer = playerPosition.position - transform.position;
            rb.velocity = directionToPlayer.normalized * movementSpeed;
        } else
        {
            rb.velocity = Vector2.zero;
        }
    }

    void PatrolState()
    {
        while (transform.position != destinationWaypoint)
        { 
            Vector3 directionToWaypoint = destinationWaypoint - transform.position;
            // Will finish later
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
