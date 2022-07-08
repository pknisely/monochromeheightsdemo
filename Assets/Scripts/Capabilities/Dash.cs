using System.Collections;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [SerializeField] private PlayerController input;
//    [SerializeField, Range(0f, 10f)] private float dashDistanceAir = 5f;
    [SerializeField, Range(0, 50f)] private float dashForceAir = 40f;
//    [SerializeField, Range(0f, 10f)] private float dashDistanceGround = 3f;
//    [SerializeField, Range(0, 50f)] private float dashForceGround = 30f;
    [SerializeField, Range(0f, 5f)] private float dashCooldown = 2f;

    private Vector2 velocity;
    private Vector2 dashingDirection;
    private Rigidbody2D body;
    private Ground ground;
    private Move move;
    private Jump jump;

    private bool onGround;
    private bool desiredDash; // bool to check whether a dash is requested by input
    private bool canDash = true; // bool to check whether or not you can dash, can only dash once in the air
    private bool isDashing; // bool to check whether currently dashing

    // Audiosource for SFX
    [SerializeField] private AudioController audioController;



    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        move = GetComponent<Move>();
        jump = GetComponent<Jump>();
 //       defaultGravityScale = 1f;
        isDashing = false;
    }

    // Update is called once per frame
    void Update()
    {
        desiredDash |= input.RetrieveDashInput();

    }

    private void FixedUpdate()
    {
        onGround = ground.GetOnGround();
        velocity = body.velocity;

        if (onGround)
            canDash = true;

        if (desiredDash)
        {
            desiredDash = false;
            DashAction(move.GetFacingRight());
        }
    }

    private void DashAction(bool facingRight)
    {
        float direction = -1;
        if (facingRight)
            direction = 1;

        if (canDash && !isDashing && !onGround)
        {
            canDash = false;
            isDashing = true;
            dashingDirection.x = direction;
            dashingDirection.y = 0f;
            body.gravityScale = 0f;
            body.velocity = dashingDirection.normalized * dashForceAir;
            body.velocity = new Vector2(0f, 0f);
            StartCoroutine(DashCooldown());
            //            body.AddForce(new Vector2(dashDistanceAir * direction, 0), ForceMode2D.Impulse);
            audioController.sfxAudioSource[3].Play();
//            body.gravityScale = defaultGravityScale;
        }
 /*       else if (canDash && !isDashing && onGround)
        {
            canDash = false;
            isDashing = true;
            velocity.x = dashForceGround * direction;
            body.velocity = velocity;
            body.gravityScale = 0f;
            StartCoroutine(DashCooldown());
            body.AddForce(new Vector2(dashDistanceGround * direction, 0), ForceMode2D.Impulse);
            audioController.sfxAudioSource[3].Play();
 //           body.gravityScale = defaultGravityScale;
        }*/
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
        body.gravityScale = jump.GetDownwardMovementMultiplier();
    }

    public bool GetIsDashing()
    {
        return isDashing;
    }

    public bool GetCanDash()
    {
        return canDash;
    }

}
