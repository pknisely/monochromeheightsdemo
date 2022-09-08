using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    // This is a controller for an area of gamespace, particularly right now focused on wind elements which add force to the player character
    // and cause them to move in a certain direciton


    [SerializeField] private bool windEffectOn = true;                          // This boolean variable determines if this area has the wind effect turned on or off
    [SerializeField] private Direction currentWindDirection = Direction.East;   // This Direction variable is the current wind direction of the four possible
    [SerializeField] private float forceMagnitude = 10;                         // This float variable determines the strength / magnitude of the wind
    [SerializeField] private Rigidbody2D target;                                // This Rigidbody2D variable gives a target for the wind effect so it only affects the character's rigidbody

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (windEffectOn)
        {

        }
    }

    enum Direction { North, South, East, West };
}
