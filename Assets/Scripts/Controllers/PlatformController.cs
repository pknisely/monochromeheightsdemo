using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlatformController : MonoBehaviour
{
    // Boolean variables to determine what kind of platform it is
    [SerializeField] private bool isMoving = false;
    [SerializeField] private bool isBouncy = false;
    [SerializeField] private bool isSlippery = false;
    [SerializeField] private bool isFalling = false;
    [SerializeField] private bool isBreakable = false;
    [SerializeField] private bool isGear = false;


    // Variables for a moving platform
    [SerializeField] private List<Transform> waypoints;         // List for the transforms for the waypoints for a moving platform
    [SerializeField] private float moveSpeed;                   // Movement speed variable for a moving platform
    [SerializeField] private int target;                        // Variable for the current target transform the platform is moving to

    // Variables for breakable platform
    [SerializeField] private float timeToBreak = 3;              // Amount of time until platform breaks after stepping on it     
    private float breakTimer = 0;                                      // Timer variable used to compare time to the breakTimer
    private bool steppedOnBreakable = false;


    // Variables for gear platform
    [SerializeField] private float rotationDirection;           // Positive 1 for rotating clockwise, negative 1 for counterclockwise
    [SerializeField] private float rotationSpeed;               // 

    // Variables for falling platform

    [SerializeField] private float fallSpeed = .08f;
    private bool steppedOnFalling = false;


    private void Awake()
    {
        if (isBouncy)
        {
            // Add logic to change physics material to bouncy...somehow
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
            transform.position = Vector3.MoveTowards(transform.position, waypoints[target].position, moveSpeed * Time.deltaTime);

        if (breakTimer > timeToBreak)
        {
            Debug.Log("Platform SHould Break!");
            gameObject.SetActive(false);
        }

        if (isGear)
        {
            RotateGear();
        }

    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isMoving)
        {
            if (transform.position == waypoints[target].position)
            {
                if (target == waypoints.Count - 1)
                    target = 0;
                 else
                    target += 1;
             }
        }

        if (steppedOnBreakable)
        {
            breakTimer += Time.deltaTime;
          //  Debug.Log("Got to addition");
        }

        //Debug.Log(breakTimer);
        //Debug.Log(steppedOnBreakable);

        if (steppedOnFalling)
        {
            Debug.Log("Got to Stepped on Falling");
            //gameObject.transform.position += new Vector3(0, (-1 * fallSpeed), 0);
            //Debug.Log("Got to addition");
            gameObject.transform.GetComponent<Rigidbody2D>().MovePosition(new Vector3(transform.position.x, (transform.position.y) - (1 * fallSpeed), 0));
        }

    }


    // Currently using this for breakable platforms, to determine if the player lands on it, and then start a timer then break it once that timer reaches a point.
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isBreakable)
        {
            Debug.Log("Got to is Breakable");
            if (collision.collider.tag == "Player") ;
            {
                Debug.Log("Got to timer modification");
                steppedOnBreakable = true;
            }
        }

        if (isFalling)
        {
            if (collision.collider.tag == "Player") ;
            {
                Debug.Log("Got to is Falling");
                //gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                steppedOnFalling = true;
            }

        }

    }

    private void RotateGear()
    {
        transform.Rotate(0, 0, rotationSpeed * rotationDirection);
    }

}
