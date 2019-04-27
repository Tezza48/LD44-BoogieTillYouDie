using UnityEngine;

public class Fight : MonoBehaviour 
{
	// Characters involved in this fight
	public Character[] characters;

	const float FIGHT_LENGTH = 5.0f;

	void Update()
	{

	}

	void OnGUI()
	{
		GUILayout.BeginVertical();

		foreach (Character character in characters)
		{
			GUILayout.Box(character.CurrentDanceAmount.ToString("N2"), GUILayout.Width(character.CurrentDanceAmount * 10));
		}

		GUILayout.EndVertical();
	}
}