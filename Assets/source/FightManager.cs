using UnityEngine;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
	public static FightManager instance;
	private List<Fight> activeFights;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
		else 
		{
			Destroy(gameObject);
		}

		activeFights = new List<Fight>();
	}

	void Update()
	{
		List<Fight> finishedFights = new List<Fight>();
		foreach (Fight fight in activeFights)
		{
			if (fight.IsFightOver())
			{
				fight.GetWinner().Win();
				var losers = fight.GetLosers();
				foreach (var loser in losers)
				{
					loser.Kill();
				}
				finishedFights.Add(fight);	
			}
		}
		foreach (Fight fight in finishedFights)
		{
			activeFights.Remove(fight);
		}
	}

	public static void StartFight(Character[] characters, float fightlength = 5.0f)
	{
		if (instance == null) 
		{
			Debug.Log("FightManager Instance has not been created yet");
			return;
		}

		foreach (var character in characters)
		{
			character.IsFighting = true;
		}

		instance.activeFights.Add(new Fight(characters, fightlength));
	}

	void OnDestroy() {
		if (this == FightManager.instance)
		{
			FightManager.instance = null;
		}
	}

	void OnGUI()
	{
		foreach (var fight in activeFights)
		{
			GUILayout.BeginHorizontal();
			fight.OnGUI();
			GUILayout.EndHorizontal();
		}
	}
}
