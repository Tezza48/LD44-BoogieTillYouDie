using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float power;
	public float danceDecay = 5.0f;

	// Accumulation of tapping power you've built up
	protected float currentDanceAmount;

	protected bool isFighting;

	public float CurrentDanceAmount { get => currentDanceAmount; }
	public bool IsFighting { get => isFighting; set => isFighting = value; }

	protected void decayDance()
	{
		currentDanceAmount -= danceDecay * Time.deltaTime;
		currentDanceAmount = Mathf.Max(currentDanceAmount, 0);
	}

	public virtual void Kill()
	{
		Destroy(gameObject);
	}
}
