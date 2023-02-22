using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    #region Variables
    private Enemy enemy;
    #endregion

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            enemy.DetectPlayer();
            enemy.GetPlayer(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            enemy.ResetDetectPlayer();
            Debug.Log("Exit");
        }
    }

    bool IsPlayer(GameObject obj)
    {
        return obj.tag == "Player";
    }
}
