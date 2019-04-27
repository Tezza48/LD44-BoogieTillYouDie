using UnityEngine;
using System.Collections;

public class PlayerCharacter: Character
{

	private void Update()
	{
		bool doDance = Input.GetKeyDown(KeyCode.UpArrow) ||
				Input.GetKeyDown(KeyCode.DownArrow) ||
				Input.GetKeyDown(KeyCode.LeftArrow) ||
				Input.GetKeyDown(KeyCode.RightArrow);

		if (isFighting) 
		{
			decayDance();
		}
		else
		{
			currentDanceAmount = 0.0f;
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

	void OnTriggerEnter2D(Collider2D collider)
	{
		var character = collider.gameObject.GetComponent<Character>();
		if (character != null) {
			if (!character.IsFighting && !IsFighting)
			FightManager.StartFight(new Character[] {this, character});
		}
	}
}