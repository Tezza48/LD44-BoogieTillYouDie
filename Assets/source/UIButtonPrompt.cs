using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtonPrompt : MonoBehaviour
{
	public Transform worldTarget;

	private RectTransform trans;
    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        trans.position = Camera.main.WorldToScreenPoint(worldTarget.position);
    }
}
