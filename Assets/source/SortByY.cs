using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SortByY : MonoBehaviour
{

	public SortingGroup group; 

	void Start()
	{
		group = GetComponent<SortingGroup>();
	}

    // Update is called once per frame
    void Update()
    {
        group.sortingOrder = Mathf.RoundToInt(-transform.position.y * 10.0f);
    }
}
