using System.Collections;
using UnityEngine;

public class Jump : MonoBehaviour
{

    [SerializeField] private PlayerController input; // reference to the PlayerController which gets input
    [SerializeField, Range(0f, 10f)] private float jumpHeight = 3f; // variable to calculate jump height
    [SerializeField, Range(0, 5)] private int maxAirJumps = 0; // number of air jumps player can perform (double jumps)
    [SerializeField, Range(0f, 10f)] private float downwardMovementMultiplier = 3f; // when falling, adds multiplier to gravity
    [SerializeField, Range(0f, 10f)] private float upwardMovementMultiplier = 3f; // when moving upward (and holding button) adds multiplier to gravity
    [SerializeField, Range(0f, 10f)] private float lowUpwardMovementModifier = 2f; // when moving upward (and not holding button) adds multiplier to gravity
    [SerializeField, Range(0f, 5f)] private float jumpCoolDown = .25f; // jump cool down timer

    private Vector2 velocity; // Vector2 to store velocity in
    private Rigidbody2D body; // reference to player's Rigidbody2D
    private Ground ground; // reference to the Ground scrip
    private Hover hover;

    private int jumpPhase; // keeps track of which phase of a double jump (or multi jump)
    private float defaultGravityScale; // the default gravity scale, set in the awake method

    private bool desiredJump; // boolean to track if a jump has been requested by user input
    private bool onGround; // boolean to check if currently on the ground

    [SerializeField] private AudioController audioController; // Audiosource for SFX

    private bool isJumping = false; // Boolean to track whether you're currently jumping


    // Coyote Time variables, coyoteTime is the amount of seconds after being on a platform you can still jump, the counter keeps track of it

    [SerializeField] private float coyoteTime = 0.2f;        
    private float coyoteTimeCounter;

    // Jump buffer variable, allows you to press jump just before landing and still jump

    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter = 0;


    // Awake is called on leading
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        hover = GetComponent<Hover>();

        defaultGravityScale = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        desiredJump |= input.RetrieveJumpInput(); 

        // Not sure this is working, maybe move to the fixedupdate and also verify, but it's meant to allow you to jump just before you touch the ground and keep a counter for that
        if (input.player.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if (onGround)
        {
            jumpPhase = 0;
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (desiredJump)
        {
            desiredJump = false;
            JumpAction();
        }

        // This logic handles the vertical velocity of the character, based on jump and hover inputs and whether they are moving upward or downward
        if (body.velocity.y > 0 && input.player.GetButton("Jump"))
        {
            body.gravityScale = upwardMovementMultiplier;
        }
        else if (body.velocity.y > 0 && !input.player.GetButton("Jump"))
        {
            body.gravityScale = lowUpwardMovementModifier;
        }
        else if (body.velocity.y == 0 && input.player.GetButton("Hover") && hover.GetHoverCounter() > 0f)
        {
            body.velocity = new Vector2(0f, 0f);
            body.gravityScale = 0;
        }
        else if (body.velocity.y == 0)
        {
            body.gravityScale = defaultGravityScale;
        }
        else if (body.velocity.y < 0)
        {
            body.gravityScale = downwardMovementMultiplier;
        }

        // resets the velocity of the character to the velocity Vector2 script which can be altered in jump (or by the hover input)
        body.velocity = velocity;
    }

    private void JumpAction()
    {
        if ((coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping) || jumpPhase < maxAirJumps)
        {
            jumpPhase += 1;
            float jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * jumpHeight);

            if (velocity.y > 0f)
            {
                jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            }

            audioController.sfxAudioSource[0].Play();

            velocity.y += jumpSpeed;

            coyoteTimeCounter = 0f;
            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }

    }
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(jumpCoolDown);
        isJumping = false;
    }

    public float GetDownwardMovementMultiplier()
    {
        return downwardMovementMultiplier;
    }
}
