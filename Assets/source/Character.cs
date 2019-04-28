using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float power;
	public float danceDecay = 5.0f;

	// Accumulation of tapping power you've built up
	protected float currentDanceAmount;
	public Sprite defaultSprite;
	public Sprite[] danceMoveSprites;
	protected SpriteRenderer spriteRenderer;

	protected bool isFighting;
	protected float lastDanceTime;

	public float CurrentDanceAmount { get => currentDanceAmount; }
	public bool IsFighting { get => isFighting; set => isFighting = value; }

	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
		StartCoroutine(handleResetSprite());
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
		Destroy(gameObject);
	}

	public virtual void Win()
	{
		isFighting = false;
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
