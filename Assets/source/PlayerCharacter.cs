using UnityEngine;

public class PlayerCharacter: Character
{
	private void Update()
	{
		if (isFighting) 
		{
			if (Input.GetKeyDown(KeyCode.UpArrow) ||
				Input.GetKeyDown(KeyCode.DownArrow) ||
				Input.GetKeyDown(KeyCode.LeftArrow) ||
				Input.GetKeyDown(KeyCode.RightArrow))
			{
				currentDanceAmount += power;
				Debug.Log("Player is Dancing");
			}

			decayDance();
		}
		else
		{
			currentDanceAmount = 0.0f;
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