using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerCharacter: Character
{
	public UIButtonPrompt danceButtonPrompt;
	public bool doInteraction = false;

	public CameraFollow cameraFollow;
	public AudioSource battleAudio;

	void Start()
	{
		battleAudio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		doInteraction = Input.GetKeyDown(KeyCode.F);

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

			SelectRandomDanceSprite();
		}

		Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),
			Input.GetAxisRaw("Vertical"));

		if (!isFighting)
			transform.position += (Vector3)input.normalized * Time.deltaTime * 1.5f;
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
					StartFightingWith(character);
				}

			}
		}
	}

	void OnTriggerExit2D(Collider2D collider)
	{
		danceButtonPrompt.gameObject.SetActive(false);
	}

	public override void Kill()
	{
		StartCoroutine(PerformDeathActions());
	}

	public override void Win()
	{
		base.Win();
		battleAudio.loop = false;
	}

	private void StartFightingWith(Character other)
	{
		FightManager.StartFight(new Character[] {this, other}, 5.0f);
		battleAudio.loop = true;
		battleAudio.Play();
	}

	private IEnumerator PerformDeathActions()
	{

		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("MenuScene");
	}
}