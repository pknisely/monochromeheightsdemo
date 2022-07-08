using UnityEngine;
using Rewired;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int playerID = 0;
    [SerializeField] public Player player;

    public void Start()
    {
        player = ReInput.players.GetPlayer(playerID);
    }

    public float RetrieveMoveInput()
    {
        return player.GetAxisRaw("MoveHorizontal");
    }
    public bool RetrieveJumpInput()
    {
        return player.GetButtonDown("Jump");
    }
    public bool RetrieveDashInput()
    {
        return false;
    }
    public bool RetrieveHoverInput()
    {
        return player.GetButtonDown("Hover");
    }
    public bool RetrieveSwapForwardInput()
    {
        return player.GetButtonDown("SwapForward");
    }
    public bool RetrieveSwapBackwardInput()
    {
        return player.GetButtonDown("SwapBackward");
    }
    public bool RetrieveSwapAltInput()
    {
        return false;
    }
}
