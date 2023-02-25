using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOMNOM : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<AudioSource>().PlayDelayed(0.75f);
    }
}
