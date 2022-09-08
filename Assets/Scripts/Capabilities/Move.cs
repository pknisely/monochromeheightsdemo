using UnityEngine;

public class Move : MonoBehaviour
{

    [SerializeField] private PlayerController input;
    [SerializeField, Range(0f, 100f)] private float maxSpeed = 4f;
    [SerializeField, Range(0f, 100f)] private float maxAcceleration = 35f;
    [SerializeField, Range(0f, 100f)] private float maxAirAcceleration = 20f;
    [SerializeField, Range(0f, 100f)] private float maxDeceleration = 20f;

    private Vector2 direction;
    private Vector2 desiredVelocity;
    private Vector2 velocity;
    private Rigidbody2D body;
    private Ground ground;

    [SerializeField] private MovingPlatformCheck movingPlatformCheck; // a reference to the MovingPlatformCheck to determine if on a moving platform

 //   private Dash dash;

    private float maxSpeedChange;
    private float acceleration;
    private bool onGround;
    private bool facingRight = true; // bool for whether or not facing right or not

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
//        dash = GetComponent<Float>();
    }

    // Update is called once per frame
    void Update()
    {
 //       if (!dash.GetIsDashing())
 //       {
            direction.x = input.RetrieveMoveInput();
            desiredVelocity = new Vector2(direction.x, 0f) * Mathf.Max(maxSpeed - ground.GetFriction(), 0f);

 //       }

    }

    private void FixedUpdate()
    {

        onGround = ground.GetOnGround();
        velocity = body.velocity;

        acceleration = onGround ? maxAcceleration : maxAirAcceleration;
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        body.velocity = velocity;

        /*
        if (movingPlatformCheck.GetOnPlatform() == true && onGround)
            body.velocity = velocity + movingPlatformCheck.GetHit().velocity;
        else
            body.velocity = velocity;
        */ 

        if (velocity.x < 0f && facingRight)                 // I COULD change these to direction.x instead of velocity.x and then it'd swap the direction of the character when sliding the other way?
        {
            Flip();
        }
        else if (velocity.x > 0f && !facingRight)
        {
            Flip();
        }

//        Debug.Log(movingPlatformCheck.GetOnPlatform());

    }

    private void Flip()
    {
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }

}
