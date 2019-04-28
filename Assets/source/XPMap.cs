using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/XP Map")]
public class XPMap : ScriptableObject
{
	[Serializable]
	public struct Level
	{
		public float power;
		public float reward;
		// XP to next Level
		public float nextLevelRequirement;
	}

	public Level[] levels;

	public float GetFightLength(int levelA, int levelB)
	{
		int difference = levelB - levelA;
		return Mathf.Max(difference * 1.4f + 5, 1);
	}

	public void PrintLengths()
	{
		var levels = new Vector2Int[] 
		{
			new Vector2Int(0, 0),
			new Vector2Int(1, 0),
			new Vector2Int(2, 0),
			new Vector2Int(3, 0),
			new Vector2Int(4, 0),
			new Vector2Int(5, 0),
			new Vector2Int(0, 1),
			new Vector2Int(0, 2),
			new Vector2Int(0, 3),
			new Vector2Int(0, 4),
			new Vector2Int(0, 5),
			new Vector2Int(10, 0)
		};

		foreach (var item in levels)
		{
			Debug.Log((item.x - item.y) + " Will fight for " + GetFightLength(item.x, item.y) + " seconds");
		}
	}
}
