using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class Respawn : MonoBehaviour
{
    private bool respawnNow = false;
    [SerializeField] private Vector2 playerPosition;                // Vector for the player's current position
    [SerializeField] private Vector2 currentRespawnPosition;        // Vector for the current respawn point (aka last checkpoint)
    [SerializeField] private GameObject playerCharacter;

    // Rewired references for the respawn button
    [SerializeField] private int playerID = 0;
    [SerializeField] private Player player;

    // Start is called before the first frame update
    void Start()
    {
        currentRespawnPosition = playerCharacter.transform.position;
        respawnNow = false;
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        if (respawnNow == true)
        {
            // Logic to move the character goes here
            playerCharacter.transform.position = currentRespawnPosition;

            // Reset respawnNow to false
            respawnNow = false;
        }
        
        if (player.GetButtonDown("Respawn") == true)
        {
           respawnNow = true;
        }

    }

    // Check to see if player falls below specific area and then respawn player at last checkpoint

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            respawnNow = true;
        }
    }

    // Setter function for the current respawn point, takes in a Vector 2 of a new position and sets the current respawn point to that position

    public void setRespawnPosition(Vector2 newPosition)
    {
        currentRespawnPosition = newPosition;
    }


}
