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
				var losers = fight.GetLosers();
				var xp = 0.0f;
				foreach (var loser in losers)
				{
					xp += loser.xpMap.levels[loser.level].reward;
					loser.Kill();
				}
				fight.GetWinner().Win(xp);
				finishedFights.Add(fight);	
			}
		}
		foreach (Fight fight in finishedFights)
		{
			activeFights.Remove(fight);
		}
	}

	public static Fight? StartFight(Character[] characters, float fightlength = 5.0f)
	{
		if (instance == null) 
		{
			Debug.Log("FightManager Instance has not been created yet");
			return null;
		}

		foreach (var character in characters)
		{
			character.IsFighting = true;
		}

		instance.activeFights.Add(new Fight(characters, fightlength));
		return instance.activeFights[instance.activeFights.Count - 1];
	}

	void OnDestroy() {
		if (this == FightManager.instance)
		{
			FightManager.instance = null;
		}
	}

#if UNITY_EDITOR
	void OnGUI()
	{
		foreach (var fight in activeFights)
		{
			GUILayout.BeginHorizontal();
			fight.OnGUI();
			GUILayout.EndHorizontal();
		}
	}
#endif
}
