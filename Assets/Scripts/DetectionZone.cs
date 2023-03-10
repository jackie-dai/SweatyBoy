using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    #region Variables
    private Enemy enemy;
    private Animator animator;
    private bool transformed = false;
    [SerializeField]
    public AudioSource growlSFX;
    #endregion

    void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        animator = GetComponentInParent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            enemy.DetectPlayer();
            enemy.GetPlayer(other.transform);
            if (!transformed)
            {
                Debug.Log("SDF");
                growlSFX.Play();
                transformed = true;
            }
            animator.SetTrigger("Transform");
            other.GetComponent<PlayerController>().chased = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other.gameObject))
        {
            enemy.ResetDetectPlayer();
            Debug.Log("Exit");
            other.GetComponent<PlayerController>().chased = false;
        }
    }

    bool IsPlayer(GameObject obj)
    {
        return obj.tag == "Player";
    }
}
