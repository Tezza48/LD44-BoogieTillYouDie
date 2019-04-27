using UnityEngine;
using System;
using System.Collections.Generic;

public struct Fight
{
	// Characters involved in this fight
	public Character[] characters;

	private float fightLength;

	private float startTime;

	public Fight(Character[] characters, float fightLength = 5.0f)
	{
		this.characters = characters; 
		this.fightLength = fightLength;
		startTime = Time.time;
	}

	// Must be called by a monobehaviour in OnGUI
	public void OnGUI()
	{
		GUILayout.BeginVertical();
		
		GUILayout.Label("Fight: " + (Time.time - startTime).ToString("N2"));

		foreach (Character character in characters)
		{
			GUILayout.Box(character.CurrentDanceAmount.ToString("N2"), GUILayout.Width(character.CurrentDanceAmount * 10));
		}

		GUILayout.EndVertical();
	}

	public bool IsFightOver()
	{
		return Time.time > startTime + fightLength;
	}

	public Character GetWinner()
	{
		Character winner = characters[0];
		for (int i = 1; i < characters.Length; i++)
		{
			if (characters[1].CurrentDanceAmount > winner.CurrentDanceAmount)
				winner = characters[i];
		}
		return winner;
	}

	public Character[] GetLosers()
	{
		var losers = new List<Character>(characters);
		losers.Remove(GetWinner());
		return losers.ToArray();
	}
}