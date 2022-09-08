using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Respawn respawn;

    private Vector2 checkpointPosition; // Vector 2 to hold the transform of the checkpoint this script is attached to

    private void Start()
    {
        checkpointPosition = transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            respawn.setRespawnPosition(checkpointPosition);
        }
        Destroy(this);
    }

}
