using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
            GetComponentInParent<EnemyGuy>().VisionConeCollided(other);
    }
}
