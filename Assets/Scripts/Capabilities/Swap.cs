using UnityEngine;
using UnityEngine.Tilemaps;

public class Swap : MonoBehaviour
{
	[SerializeField] private PlayerController input;

	// Int keeping track of how many colors you have access to, defaults to just 1, which is black
	[SerializeField] private int canSwapNum = 1;

	// Int keeping track of which color you are;
	[SerializeField] private int currentColor = 0;

	// Audio controller for SFX
	[SerializeField] private AudioController audioController;

	// Four tilemap collider 2Ds for the tilemaps to enable or disable depending on character color
	[SerializeField] private TilemapCollider2D[] colorTilemapCollider;

	// Four spriterenderers to enable or disable depending on character color
	[SerializeField] private SpriteRenderer[] colorSpriteRenderer;

	private bool desiredSwapForward;
	private bool desiredSwapBackward;

	// Update is called once per frame
	void Update()
    {
		desiredSwapForward |= input.RetrieveSwapForwardInput();
		desiredSwapBackward |= input.RetrieveSwapBackwardInput();
	}

	private void FixedUpdate()
    {
		if (desiredSwapForward)
		{
			desiredSwapForward = false;
			SwapAction(1);
		}
		if (desiredSwapBackward)
        {
			desiredSwapBackward = false;
			SwapAction(2);
        }
	}

    // Swap color, accepts a parameter for the direction of the swap, 1 is forward, 2 is reverse
    private void SwapAction(int direction)
    {
		if (canSwapNum == 1)
			return;
		ChangeCollider(currentColor, direction);
		ChangeCharacterSprite(currentColor, direction);
		audioController.sfxAudioSource[1].Play();
	}


	// Function to change colliders and sprites in forward order, currently broken because it's all copied and pasted and the same for each case
	public void ChangeCollider(int colorNum, int direction)
	{
		// canSwapNum 2 = black/white 3 = black/white/gray and 0 means can't swap
		// colorNum 0 = black, 1 = white, 2 = gray
		// direction 1 = forward 2 = backward

		switch (canSwapNum)
		{
			case 2:
				switch (colorNum)
				{
					case 0:
						switch (direction)
						{
							case 1:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = true;
								break;

							case 2:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = true;
								break;
						}
						break;

					case 1:
						switch (direction)
						{
							case 1:
								colorTilemapCollider[0].enabled = true;
								colorTilemapCollider[1].enabled = false;
								break;

							case 2:
								colorTilemapCollider[0].enabled = true;
								colorTilemapCollider[1].enabled = false;
								break;

						}
						break;
				}
				break;

			case 3:
				switch (colorNum)
				{
					case 0:
						switch (direction)
						{
							case 1:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = true;
								colorTilemapCollider[2].enabled = false;
								break;

							case 2:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = false;
								colorTilemapCollider[2].enabled = true;
								break;

						}
						break;

					case 1:
						switch (direction)
						{
							case 1:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = false;
								colorTilemapCollider[2].enabled = true;
								break;
							case 2:
								colorTilemapCollider[0].enabled = true;
								colorTilemapCollider[1].enabled = false;
								colorTilemapCollider[2].enabled = false;
								break;
						}
						break;

					case 2:
						switch (direction)
						{
							case 1:
								colorTilemapCollider[0].enabled = true;
								colorTilemapCollider[1].enabled = false;
								colorTilemapCollider[2].enabled = false;
								break;
							case 2:
								colorTilemapCollider[0].enabled = false;
								colorTilemapCollider[1].enabled = true;
								colorTilemapCollider[2].enabled = false;
								break;
						}
						break;
				}
				break;
		}
	}

	public void ChangeCharacterSprite(int colorNum, int direction)
	{
		// canSwapNum 2 = black/white 3 = black/white/gray and 0 means can't swap
		// colorNum 0 = black, 1 = white, 2 = gray
		// direction 1 = forward 2 = backward

		switch (canSwapNum)
		{
			case 2:
				switch (colorNum)
				{
					case 0:

						switch (direction)
						{
							case 1:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = true;
								currentColor = 1;
								break;
							case 2:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = true;
								currentColor = 1;
								break;
						}
						break;

					case 1:

						switch (direction)
						{
							case 1:
								colorSpriteRenderer[0].enabled = true;
								colorSpriteRenderer[1].enabled = false;
								currentColor = 0;
								break;
							case 2:
								colorSpriteRenderer[0].enabled = true;
								colorSpriteRenderer[1].enabled = false;
								currentColor = 0;
								break;

						}
						break;
				}
				break;

			case 3:
				switch (colorNum)
                {
					case 0:
						switch (direction)
						{
							case 1:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = true;
								colorSpriteRenderer[2].enabled = false;
								currentColor = 1;
								break;
							case 2:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = false;
								colorSpriteRenderer[2].enabled = true;
								currentColor = 2;
								break;
						}
						break;

					case 1:
						switch (direction)
						{
							case 1:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = false;
								colorSpriteRenderer[2].enabled = true;
								currentColor = 2;
								break;
							case 2:
								colorSpriteRenderer[0].enabled = true;
								colorSpriteRenderer[1].enabled = false;
								colorSpriteRenderer[2].enabled = false;
								currentColor = 0;
								break;

						}
						break;
					case 2:
						switch (direction)
						{
							case 1:
								colorSpriteRenderer[0].enabled = true;
								colorSpriteRenderer[1].enabled = false;
								colorSpriteRenderer[2].enabled = false;
								currentColor = 0;
								break;
							case 2:
								colorSpriteRenderer[0].enabled = false;
								colorSpriteRenderer[1].enabled = true;
								colorSpriteRenderer[2].enabled = false;
								currentColor = 1;
								break;

						}
						break;
				}
				break;
		}
	}
}
