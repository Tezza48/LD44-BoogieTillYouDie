using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PlayerCharacter: Character
{
	public UIButtonPrompt danceButtonPrompt;
	public UIFightMeter fightMeter;
	public CameraFollow cameraFollow;
	public AudioSource battleAudio;
	public bool doInteraction = false;

	void Start()
	{
		battleAudio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		UpdateLevelDisplay();
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
				currentDanceAmount += xpMap.levels[level].power;

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

	public override void Win(float xpReward)
	{
		base.Win(xpReward);
		battleAudio.loop = false;
		fightMeter.gameObject.SetActive(false);
	}

	private void StartFightingWith(Character other)
	{
		var fightTime = xpMap.GetFightLength(this.level, other.level);
		var fight = FightManager.StartFight(new Character[] {this, other}, fightTime);
		battleAudio.loop = true;
		battleAudio.Play();
		
		fightMeter.FightToWatch = fight.Value;
		fightMeter.gameObject.SetActive(true);
	}

	private IEnumerator PerformDeathActions()
	{
		fightMeter.gameObject.SetActive(false);
		Destroy(levelText.gameObject);
		
		yield return new WaitForSeconds(2.0f);
		SceneManager.LoadScene("MenuScene");
	}
}