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
			spriteRenderer.sprite = defaultSprite;
			yield return new WaitForSeconds(1);
		}
	}
}
