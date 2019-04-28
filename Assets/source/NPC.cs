using UnityEngine;
using System.Collections;

public class NPC : Character
{
	// Speed at which this NPC can fight
	public float danceCooldown;
	
	private IEnumerator AutoDance()
	{
		while(true) {
			if(isFighting)
			{
				currentDanceAmount += xpMap.levels[level].power;
				SelectRandomDanceSprite();
			}
			else
			{
				currentDanceAmount = 0.0f;
			}
			yield return new WaitForSeconds(danceCooldown);
		}
	} 
	
	void Start()
	{
		StartCoroutine(AutoDance());
	}

	void Update()
	{
		decayDance();
	}

}