using UnityEngine;

public class FightManager : MonoBehaviour
{
	public static FightManager instance;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		}
		else 
		{
			Destroy(gameObject);
		}
	}

	void OnDestroy() {
		if (this == FightManager.instance)
		{
			FightManager.instance = null;
		}
	}
}