using UnityEngine;
using System.Collections;

public class PlayerCharacter: Character
{
	public UIButtonPrompt danceButtonPrompt;
	public bool doInteraction = false;

	public CameraFollow cameraFollow;

	private void Update()
	{
		doInteraction = Input.GetKeyUp(KeyCode.F);

		bool doDance = Input.GetKeyDown(KeyCode.UpArrow) ||
				Input.GetKeyDown(KeyCode.DownArrow) ||
				Input.GetKeyDown(KeyCode.LeftArrow) ||
				Input.GetKeyDown(KeyCode.RightArrow);

		if (isFighting) 
		{
			decayDance();
			cameraFollow.isZooming = true;
		}
		else
		{
			currentDanceAmount = 0.0f;
			cameraFollow.isZooming = false;
		}

		if (doDance)
		{
			if (isFighting)
				currentDanceAmount += power;
			spriteRenderer.sprite = danceMoveSprites[Random.Range(0, danceMoveSprites.Length)];
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical"));
		if (!isFighting)
			transform.position += (Vector3)input * Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		var character = collider.gameObject.GetComponent<Character>();
		if (character != null) {
			if (!character.IsFighting && !IsFighting)
			{
				danceButtonPrompt.gameObject.SetActive(!doInteraction);
				danceButtonPrompt.worldTarget = character.transform;

				if (doInteraction)
				{
					FightManager.StartFight(new Character[] {this, character});
				}

			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		danceButtonPrompt.gameObject.SetActive(false);
	}
}