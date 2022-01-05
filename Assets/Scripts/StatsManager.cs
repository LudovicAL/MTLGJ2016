using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {

	public Text distanceDisplay;
	public Transform currentPosition;
	public Transform objectivePosition;

	private int distance;


	// Use this for initialization
	void Start () {
		distance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		distance = (int)Mathf.Ceil(Vector3.Distance(currentPosition.position, objectivePosition.position));
		distanceDisplay.text = distance + " m";
		if (distance < 3)  {
			GameObject mainMenuObject = GameObject.Find("MainMenu");
			MainMenu mainMenu = mainMenuObject ? mainMenuObject.GetComponent<MainMenu>() : null;
			if (mainMenu != null && !mainMenu.active) {
				stats.staticSuccesses++;
				mainMenu.Activate(true);
			}
		}
	}

	//Getters and setters
	public static StatsManager getInstance() {
		return GameObject.Find("GameCanvas").GetComponent<StatsManager>();
	}

	public float getDistance() {
		return distance;
	}
}

public static class stats  {
	static public int staticController1 = 1;
	static public int staticController2 = 2;
	static public int staticSuccesses = 0;
	static public int staticFailures = 0;
}
