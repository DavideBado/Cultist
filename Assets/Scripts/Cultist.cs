using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Cultist : MonoBehaviour
{
    NavMeshHit hit;
    
    // Update is called once per frame
    void Update()
    {
        if (NavMesh.SamplePosition(transform.position, out hit, 0.1f, 8)) // Se sei sopra una zona trappola
        {
            Destroy(gameObject); // Muori
        }
    }    
}