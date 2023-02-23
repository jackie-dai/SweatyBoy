using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;
    [SerializeField]
    private float offsetX = 2f;
    [SerializeField]
    private float offsetY = -2f;

    void Update()
    {
        transform.position = new Vector3(player.position.x + offsetX, player.position.y + offsetY, -10);
    }
}
