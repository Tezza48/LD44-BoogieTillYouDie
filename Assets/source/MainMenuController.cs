using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using TMPro;

public class MainMenuController : MonoBehaviour
{
	private bool w, a, s, d;
	private int numArrowsLeft = 100;

	public TextMeshProUGUI arrowText;
	public TextMeshProUGUI wasdText;
	public GameObject startButton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        arrowText.text = numArrowsLeft.ToString() + " Dances before you can start\n(arrow keys)";
		w = w || Input.GetKeyDown(KeyCode.W);
		a = a || Input.GetKeyDown(KeyCode.A);
		s = s || Input.GetKeyDown(KeyCode.S);
		d = d || Input.GetKeyDown(KeyCode.D);

		if (Input.GetKeyDown(KeyCode.UpArrow)) numArrowsLeft--;
		if (Input.GetKeyDown(KeyCode.DownArrow)) numArrowsLeft--;
		if (Input.GetKeyDown(KeyCode.LeftArrow)) numArrowsLeft--;
		if (Input.GetKeyDown(KeyCode.RightArrow)) numArrowsLeft--;

		if ((numArrowsLeft <= 0) && w && a && s && d) startButton.SetActive(true);

		wasdText.text = "W: " + w.ToString() + "\n" + 
						"A: " + a.ToString() + "\n" + 
						"S: " + s.ToString() + "\n" + 
						"D: " + d.ToString();

    }

	public void handleStartGame()
	{
		SceneManager.LoadScene(1);
	}
}
