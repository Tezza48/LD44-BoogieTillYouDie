using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	public float power;
	public float danceDecay = 5.0f;

	// Accumulation of tapping power you've built up
	protected float currentDanceAmount;

	protected void decayDance()
	{
		currentDanceAmount -= danceDecay * Time.deltaTime;
		currentDanceAmount = Mathf.Max(currentDanceAmount, 0);
	}
	public float CurrentDanceAmount { get => currentDanceAmount; }
}
