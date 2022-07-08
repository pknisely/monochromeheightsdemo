using System.Collections;
using UnityEngine;

public class Hover : MonoBehaviour
{
    
    [SerializeField] private PlayerController input; // reference to the PlayerController which gets input
    [SerializeField] private float hoverTime = 2f;  // Max hover time
    private float hoverCounter; // variable to track how long you've hovered

    private Rigidbody2D body;   // reference to player's Rigidbody2D
    private Ground ground; // reference to Ground script

    private bool onGround; // bool to check whether on ground or not
    private bool desiredHover; // bool to check whether a dash is requested by input
    private bool canHover = true; // bool to check whether or not you can dash, can only dash once in the air
    private bool isHovering; // bool to check whether currently dashing

    
    [SerializeField] private AudioController audioController; // Audiosource for SFX

    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        isHovering = false;
    }

    // Update is called once per frame
    void Update()
    {
        desiredHover |= input.RetrieveHoverInput();
    }

    private void FixedUpdate()
    {
        // Checks if player is on the ground, if so, resets whether you can Hover and if you're hovering and the hover counter
        onGround = ground.GetOnGround();
        if (onGround)
        {
            canHover = true;
            isHovering = false;
            hoverCounter = hoverTime;
        }

        // Checks if you're currently hovering, and if so counts down on the timer
        if (isHovering)
            hoverCounter -= Time.deltaTime;
        
        // Checks if you want to hover and if you can
        if (desiredHover && canHover && !isHovering && !onGround && hoverCounter > 0f)
        {
            desiredHover = false;
            canHover = false;
            isHovering = true;
            body.velocity = new Vector2(0f, 0f);
            audioController.sfxAudioSource[3].Play();                       
        }
     
    }

    public bool GetIsHovering()
    {
        return isHovering;
    }

    public bool GetCanHover()
    {
        return canHover;
    }

    public float GetHoverCounter()
    {
        return hoverCounter;
    }

} 
