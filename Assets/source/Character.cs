using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Character : MonoBehaviour
{
	public int level;
	public float currentXP;
	public float danceDecay = 5.0f;

	public XPMap xpMap;

	public GameObject deathEffect;

	// Accumulation of tapping power you've built up
	protected float currentDanceAmount;
	public Sprite defaultSprite;
	public Sprite[] danceMoveSprites;
	protected SpriteRenderer spriteRenderer;

	protected bool isFighting;
	protected float lastDanceTime;

	public GameObject UILevelDisplay;
	protected TextMeshProUGUI levelText;

	public float CurrentDanceAmount { get => currentDanceAmount; }
	public bool IsFighting { get => isFighting; set => isFighting = value; }

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(handleResetSprite());


		var display = Instantiate(UILevelDisplay);
		display.GetComponent<UITrackObjectInWorld>().worldTarget = transform;
		levelText = display.GetComponent<TextMeshProUGUI>();

		display.transform.SetParent(GameObject.Find("Canvas").transform);
	}

	protected void UpdateLevelDisplay()
	{
		levelText.text = level.ToString();
	}

	protected void decayDance()
	{
		currentDanceAmount -= danceDecay * Time.deltaTime;
		currentDanceAmount = Mathf.Max(currentDanceAmount, 0);
	}

	protected void SelectRandomDanceSprite()
	{
		spriteRenderer.sprite = danceMoveSprites[Random.Range(0, danceMoveSprites.Length)];
		transform.localScale = new Vector3(
			(Random.value > 0.5f) ? -1.0f : 1.0f,
			(Random.value > 0.99f) ? -1.0f : 1.0f, 
			1.0f);
		lastDanceTime = Time.time;
	}

	public virtual void Kill()
	{
		Instantiate(deathEffect, transform.position, Quaternion.identity);
		Destroy(gameObject);
		Destroy(levelText.gameObject);
	}

	public virtual void Win(float xpReward)
	{
		isFighting = false;
		
		currentXP += xpReward;
		if (currentXP >= xpMap.levels[level].nextLevelRequirement)
		{
			level++;
			currentXP = 0.0f;
		}
	}

	private IEnumerator handleResetSprite()
	{
		while(true)
		{
			yield return new WaitForEndOfFrame();
			if (lastDanceTime < Time.time - 1.0f)
			{
				spriteRenderer.sprite = defaultSprite;
				transform.localScale = Vector3.one;
			}
		}
	}
}
