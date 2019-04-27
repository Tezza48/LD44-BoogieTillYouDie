using UnityEngine;

public class PlayerCharacter: Character
{
	private void Update()
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

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical"));

		transform.position += (Vector3)input * Time.deltaTime;
	}
}