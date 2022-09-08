using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class MovingPlatformCheck : MonoBehaviour
{
    private bool isGrounded;
    private bool check;
    private bool onMovingPlatform;
    [SerializeField] Ground ground;
    [SerializeField] Transform playerTransform;

    // Rewired info
    [SerializeField] private int playerID = 0;
    [SerializeField] public Player player;

    private RaycastHit2D hit;

    /*

    NOTE:  There's weird stuttering when on the platform and moving (or even not moving), that's fixed in the air by the unparenting part, 
    but the parenting part is what makes it work so... maybe camera? Maybe not

    */

    public void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = ground.GetOnGround();

        if (isGrounded != true)
        {
            check = false;
            if (player.GetAxisRaw("MoveHorizontal") > .25f || player.GetAxisRaw("MoveHorizontal") < -.25f)
            {
                playerTransform.SetParent(null);
            }
        }

        if (check != true)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, .125f);

            if (hit.collider != null)
            {
                if (hit.collider.CompareTag("MovingPlatform"))
                {
                    playerTransform.SetParent(hit.transform);
                    onMovingPlatform = true;
                }
                else
                {
                    playerTransform.SetParent(null);
                }

                check = true;
            }
        }

    }

    public bool GetOnPlatform()
    {
        return onMovingPlatform;
    }

    public Rigidbody2D GetHit()
    {
        return hit.rigidbody;
    }

}
